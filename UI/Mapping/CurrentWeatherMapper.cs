using Domain.Dto;
using Domain.Enumeration;
using UI.ViewModel.WeatherForecast;

namespace UI.Mapping
{
	public static class CurrentWeatherMapper
	{
		public static CurrentWeatherViewModel ToViewModel(this CurrentWeatherDto currentWeather)
		{
			return new CurrentWeatherViewModel()
			{
				Time = currentWeather.Time,
				IsDay = currentWeather.IsDay,
				CloudCover = currentWeather.CloudCover,
				Pressure = currentWeather.Pressure,
				Rain = currentWeather.Rain,
				Showers = currentWeather.Showers,
				Snowfall = currentWeather.Snowfall,
				Temperature = currentWeather.Temperature,
				WeatherCode = (WeatherCode)currentWeather.WeatherCode,
				WindSpeed = currentWeather.WindSpeed,
			};
		}
	}
}
