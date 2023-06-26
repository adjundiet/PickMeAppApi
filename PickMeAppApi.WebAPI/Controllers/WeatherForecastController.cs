using MediatR;
using Microsoft.AspNetCore.Mvc;
using PickMeAppApi.Application.Queries;

namespace PickMeAppApi.WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IMediator _mediatr;
    private readonly ILogger<WeatherForecastController> _logger;
    
    public WeatherForecastController(IMediator mediatr, ILogger<WeatherForecastController> logger)
    {
        _mediatr = mediatr;
        _logger = logger;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IActionResult> Get()
    {
        return Ok(await _mediatr.Send(new GetWeatherForecastQuery(), CancellationToken.None));
    }
}