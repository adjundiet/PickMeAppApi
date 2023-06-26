using MediatR;

namespace PickMeAppApi.Application.Queries;

public record GetWeatherForecastQuery : IRequest<IEnumerable<GetWeatherForecastResponse>>
{
}