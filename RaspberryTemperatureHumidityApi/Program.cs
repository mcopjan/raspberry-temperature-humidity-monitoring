using Microsoft.AspNetCore.Mvc;
using RaspberryTemperatureHumidityApi.Model;
using System.Collections.Concurrent;

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

ConcurrentBag<RoomStats> RoomStatistics = new ConcurrentBag<RoomStats>();
ConcurrentBag<string> RoomNames = new ConcurrentBag<string>();


app.MapGet("/roomstats/{roomName}", (string roomName) =>
{
    return RoomStatistics.Where(r=>r.RoomName.Equals(roomName)).ToList();
})
.WithName("GetRoomStats");

app.MapGet("/roomnames", () =>
{
    return RoomNames.ToList();
    //return new List<string>() { "room1","room2" };
})
.WithName("GetRoomNames");

app.MapPost("/roomstats", async ([FromBody] RoomStats stats) =>
{
    try
    {
        if (RoomNames.All(r => !r.Equals(stats.RoomName)))
            RoomNames.Add(stats.RoomName);  

        stats.CreatedAt = DateTime.Now;
        await Task.Run(() => RoomStatistics.Add(stats));
        Results.Ok(stats);  
    }
    catch (Exception)
    {
        Results.Problem("Error creating new employee record");
    }
})
.WithName("PostRoomStats");

// simple healthcheck
app.MapGet("/hello", async context =>
{
    context.Response.StatusCode = 200;
    await context.Response.WriteAsync("Api is up and running.");
})
.WithName("Hello");

app.Run();
