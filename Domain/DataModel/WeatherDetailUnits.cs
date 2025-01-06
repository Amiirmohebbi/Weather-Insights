using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DataModel
{
	public class WeatherDetailUnits
	{
		public int Id { get; set; }
		public int WeatherDetailId { get; set; }
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
