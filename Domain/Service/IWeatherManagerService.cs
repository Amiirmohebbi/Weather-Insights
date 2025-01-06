using Domain.Dto;

namespace Domain.Service
{
	public interface IWeatherManagerService
	{
		Task<WeatherForecastDto> GetWeatherAsync(WeatherFetcherDto weatherFetcherDto);
		Task<WeatherForecastDto> GetLastWeatherAsyncByLocation(float latitude, float longitude);
	}
}
