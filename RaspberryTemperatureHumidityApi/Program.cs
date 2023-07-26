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


app.MapGet("/roomstats", () =>
{
    return RoomStatistics.ToList();
})
.WithName("GetRoomStats");

app.MapPost("/roomstats", async ([FromBody] RoomStats stats) =>
{
    try
    {
        await Task.Run(() => RoomStatistics.Add(stats));
        Results.Ok(stats);  
    }
    catch (Exception)
    {
        Results.Problem("Error creating new employee record");
    }
})
.WithName("PostRoomStats");

app.Run();
