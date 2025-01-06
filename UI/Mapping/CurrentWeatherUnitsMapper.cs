using Domain.Dto;
using UI.ViewModel.WeatherForecast;

namespace UI.Mapping
{
	public static class CurrentWeatherUnitsMapper
	{
		public static CurrentWeatherUnitsViewModel ToViewModel(this CurrentWeatherUnitsDto units)
		{
			return new CurrentWeatherUnitsViewModel()
			{
				Time = units.Time,
				CloudCover = units.CloudCover,
				Pressure = units.Pressure,
				Rain = units.Rain,
				Showers = units.Showers,
				Snowfall = units.Snowfall,
				Temperature = units.Temperature,
				WindSpeed = units.WindSpeed
			};
		}
	}
}
