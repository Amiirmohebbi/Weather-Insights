using Domain.Dto;

namespace Domain.ExternalService
{
	public interface IOpenMeteoService
	{
		Task<WeatherForecastDto> GetWeatherByLocationAsync(float latitude, float longitude);
	}
}
