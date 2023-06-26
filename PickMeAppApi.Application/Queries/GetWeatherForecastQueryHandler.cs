using MediatR;

namespace PickMeAppApi.Application.Queries;

public class GetWeatherForecastQueryHandler : IRequestHandler<GetWeatherForecastQuery, IEnumerable<GetWeatherForecastResponse>>
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    
    public Task<IEnumerable<GetWeatherForecastResponse>> Handle(GetWeatherForecastQuery request, CancellationToken cancellationToken)
    {
        return Task.FromResult<IEnumerable<GetWeatherForecastResponse>>(Enumerable.Range(1, 5).Select(index => new GetWeatherForecastResponse()
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray());
    }
}