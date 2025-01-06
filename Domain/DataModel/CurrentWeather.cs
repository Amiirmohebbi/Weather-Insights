namespace Domain.DataModel
{
	public class CurrentWeather
	{
		public DateTime Time { get; set; }

		public int Interval { get; set; }

		public float Temperature { get; set; }

		public int WeatherCode { get; set; }

		public float WindSpeed { get; set; }

		public int WindDirection { get; set; }

		public float WindGusts { get; set; }

		public int RelativeHumidity { get; set; }

		public float ApparentTemperature { get; set; }

		public bool IsDay { get; set; }

		public float Precipitation { get; set; }

		public float Rain { get; set; }

		public float Showers { get; set; }

		public float Snowfall { get; set; }

		public int Cloudcover { get; set; }

		public float Pressure { get; set; }

		public float SurfacePressure { get; set; }
	}
}
