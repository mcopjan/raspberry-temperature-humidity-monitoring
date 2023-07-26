using System.Collections.Concurrent;
using Microsoft.AspNetCore.Mvc;
using TempHumApi.Model;

namespace TempHumApi.Controllers;

[ApiController]
[Route("[controller]")]
public class RoomsStatisticsController : ControllerBase
{
    public static ConcurrentBag<RoomStats> RoomStatistics = new ConcurrentBag<RoomStats>();


    private readonly ILogger<RoomsStatisticsController> _logger;

    public RoomsStatisticsController(ILogger<RoomsStatisticsController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public List<RoomStats> Get()
    {
        return RoomStatistics.ToList();
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] RoomStats stats)
    {
        try
        {
            await Task.Run(() => RoomStatistics.Add(stats));
            return StatusCode(StatusCodes.Status200OK);
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "Error creating new employee record");
        }
    }
}

