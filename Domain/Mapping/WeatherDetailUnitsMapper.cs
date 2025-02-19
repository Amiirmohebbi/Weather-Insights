﻿using Domain.DataModel;
using Domain.Dto;

namespace Domain.Mapping
{
	public static class WeatherDetailUnitsMapper
	{
		public static WeatherDetailUnits ToDataModel(this CurrentWeatherUnitsDto currentWeatherUnitsDto, Guid weatherDetailGuid)
		{
			return new WeatherDetailUnits() 
			{
				WeatherDetailGuid = weatherDetailGuid,
				CloudCover = currentWeatherUnitsDto.CloudCover,
				Pressure = currentWeatherUnitsDto.Pressure,
				Rain = currentWeatherUnitsDto.Rain,
				Showers = currentWeatherUnitsDto.Showers,
				Snowfall = currentWeatherUnitsDto.Snowfall,
				Temperature = currentWeatherUnitsDto.Temperature,
				Time = currentWeatherUnitsDto.Time,
				WindSpeed = currentWeatherUnitsDto.WindSpeed
			};
		}
	}
}
