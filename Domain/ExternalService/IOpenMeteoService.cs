using Domain.Dto;
using System.Collections.Concurrent;

namespace Domain.ExternalService
{
	public interface IOpenMeteoService
	{
		Task<WeatherForecastDto> GetWeatherByLocationAsync(WeatherFetcherDto weatherFetcherDto);
	}
}
