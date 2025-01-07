using Domain.Enumeration;

namespace Domain.DataModel
{
	public class WeatherDetail
	{
		public WeatherDetail()
		{
			Guid = Guid.NewGuid();
		}

		public Guid Guid { get; set; }
		public Guid LocationGuid { get; set; }
		public DateTime Time { get; set; }
		public float Temperature { get; set; }
		public WeatherCode WeatherCode { get; set; }
		public float WindSpeed { get; set; }
		public bool IsDay { get; set; }
		public float Rain { get; set; }
		public float Showers { get; set; }
		public float Snowfall { get; set; }
		public int CloudCover { get; set; }
		public float Pressure { get; set; }
	}
}
