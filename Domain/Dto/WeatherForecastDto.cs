﻿namespace Domain.Dto
{
	public class WeatherForecastDto
	{
		public float Latitude { get; set; }

		public float Longitude { get; set; }

		public string Timezone { get; set; }

		public CurrentWeatherDto CurrentWeather { get; set; }

		public CurrentWeatherUnitsDto CurrentUnits { get; set; }
	}
}