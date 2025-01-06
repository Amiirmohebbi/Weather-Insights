using Domain.DataModel;
using Domain.Dto;

namespace Domain.Mapping
{
	public static class WeatherDetailMapper
	{
		public static WeatherDetail ToDataModel(this CurrentWeatherDto currentWeatherDto)
		{
			return new WeatherDetail() 
			{
				CloudCover = currentWeatherDto.CloudCover,
				IsDay = currentWeatherDto.IsDay,
				Pressure = currentWeatherDto.Pressure,
				Rain = currentWeatherDto.Rain,
				Showers = currentWeatherDto.Showers,
				Snowfall = currentWeatherDto.Snowfall,
				Temperature = currentWeatherDto.Temperature,
				Time = currentWeatherDto.Time,
				WeatherCode = currentWeatherDto.WeatherCode,
				WindSpeed = currentWeatherDto.WindSpeed
			};
		}
	}
}
