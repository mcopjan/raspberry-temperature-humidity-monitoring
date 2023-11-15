using Microsoft.AspNetCore.Mvc;
using Raspberry.Temperature.Humidity.Api.Repository;
using RaspberryTemperatureHumidityApi.Model;
using System.Collections.Concurrent;

internal class Program
{


    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        ConcurrentBag<string> RoomNames = new ConcurrentBag<string>();


        app.MapGet("/roomstats/{roomName}", (string roomName) =>
        {
            var roomStats = FileRepository.ReadRoomsStatsFromFile();
            return roomStats.Where(r => r.RoomName.Equals(roomName)).ToList();
        });

        app.MapGet("/roomnames", () =>
        {
            return RoomNames.ToList();
        });

        app.MapPost("/roomstats", ([FromBody] RoomStats stats) =>
        {
            try
            {
                if (RoomNames.All(r => !r.Equals(stats.RoomName)))
                    RoomNames.Add(stats.RoomName);

                stats.CreatedAt = DateTime.Now;
                FileRepository.WriteRoomStatsIntoFile(stats);
                Results.Ok(stats);
            }
            catch (Exception)
            {
                Results.Problem("A problem has occured while storing rooms statistics");
            }
        });

        app.MapGet("/hello", async context =>
        {
            context.Response.StatusCode = 200;
            await context.Response.WriteAsync("Api is up and running.");
        });

        app.Run();
    }
}