using Domain.Dto;

namespace Domain.Service
{
	public interface IWeatherManagerService
	{
		Task<WeatherForecastDto> GetWeatherAsync(WeatherFetcherDto weatherFetcherDto);
		Task<WeatherForecastDto> GetLastWeatherByLocationAsync(float latitude, float longitude);
		void ManageWeatherData(IEnumerable<WeatherForecastDto> weatherForecastDtos);
	}
}
