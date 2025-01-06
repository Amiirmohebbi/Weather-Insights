using Domain.Dto;
using UI.ViewModel.WeatherForecast;

namespace UI.Mapping
{
	public static class WeatherForecastMapper
	{
		public static WeatherForecastViewModel ToViewModel(this WeatherForecastDto weatherForecast)
		{
			return new WeatherForecastViewModel()
			{
				Latitude = weatherForecast.Latitude,
				Longitude = weatherForecast.Longitude,
				Timezone = weatherForecast.Timezone,
				CurrentWeather = weatherForecast.CurrentWeather.ToViewModel(),
				CurrentUnits = weatherForecast.CurrentUnits.ToViewModel()
			};
		}
	}
}
