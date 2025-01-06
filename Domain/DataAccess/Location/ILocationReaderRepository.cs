using Domain.Dto;

namespace Domain.DataAccess.Location
{
	public interface ILocationReaderRepository
	{
		Task<WeatherForecastDto> GetWeatherByLocationAsync(float latitude, float longitude);
	}
}
