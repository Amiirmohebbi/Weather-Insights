namespace Domain.DataModel
{
	public class WeatherDetailUnits
	{
		public WeatherDetailUnits()
		{
			Guid = Guid.NewGuid();
		}

		public Guid Guid { get; set; }
		public Guid WeatherDetailGuid { get; set; }
		public string Time { get; set; }
		public string Temperature { get; set; }
		public string WindSpeed { get; set; }
		public string Rain { get; set; }
		public string Showers { get; set; }
		public string Snowfall { get; set; }
		public string CloudCover { get; set; }
		public string Pressure { get; set; }
	}
}
