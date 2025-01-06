namespace UI.ViewModel.WeatherForecast
{
	public class WeatherForecastViewModel
	{
		public float Latitude { get; set; }

		public float Longitude { get; set; }

		public string Timezone { get; set; }

		public CurrentWeatherViewModel CurrentWeather { get; set; }

		public CurrentWeatherUnitsViewModel CurrentUnits { get; set; }
	}
}
