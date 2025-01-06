using Domain.Dto;
using OpenMeteo;

namespace Service.Mapping
{
	public static class CurrentWeatherUnitsMapper
	{
		public static CurrentWeatherUnitsDto ToDto(this CurrentUnits units)
		{
			return new CurrentWeatherUnitsDto()
			{
				Time = units.Time,
				CloudCover = units.Cloudcover,
				Pressure = units.Pressure_msl,
				Rain = units.Rain,
				Showers = units.Showers,
				Snowfall = units.Snowfall,
				Temperature = units.Temperature,
				WindSpeed = units.Windspeed_10m
			};
		}
	}
}
