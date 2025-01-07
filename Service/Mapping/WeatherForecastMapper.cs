using Domain.Dto;
using OpenMeteo;

namespace Service.Mapping
{
	public static class WeatherForecastMapper
	{
		public static WeatherForecastDto ToDto(this WeatherForecast weatherForecast, Guid userId)
		{
			return new WeatherForecastDto()
			{
				UserId = userId,
				Latitude = weatherForecast.Latitude,
				Longitude = weatherForecast.Longitude,
				Timezone = weatherForecast.Timezone,
				CurrentWeather = weatherForecast.Current.ToDto(),
				CurrentUnits = weatherForecast.CurrentUnits.ToDto()
			};
		}
	}
}
