namespace Domain.DataModel
{
	public class WeatherForecast
	{
		public float Latitude { get; set; }

		public float Longitude { get; set; }

		public float Elevation { get; set; }

		public float GenerationTime { get; set; }

		public int UtcOffset { get; set; }

		public string Timezone { get; set; }

		public CurrentWeather CurrentWeather { get; set; }

		public CurrentWeatherUnits CurrentUnits { get; set; }
	}
}
