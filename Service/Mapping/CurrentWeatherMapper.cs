using Domain.Dto;
using Domain.Enumeration;
using OpenMeteo;

namespace Service.Mapping
{
	public static class CurrentWeatherMapper
	{
		public static CurrentWeatherDto ToDto(this Current currentWeather)
		{
			return new CurrentWeatherDto()
			{
				Time = DateTime.Parse(currentWeather.Time),
				IsDay = currentWeather.Is_day == 1 ? true : false,
				CloudCover = currentWeather.Cloudcover.Value,
				Pressure = currentWeather.Pressure_msl.Value,
				Rain = currentWeather.Rain.Value,
				Showers = currentWeather.Showers.Value,
				Snowfall = currentWeather.Snowfall.Value,
				Temperature = currentWeather.Temperature.Value,
				WeatherCode = (WeatherCode)currentWeather.Weathercode.Value,
				WindSpeed = currentWeather.Windspeed_10m.Value,
			};
		}
	}
}
