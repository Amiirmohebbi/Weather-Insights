using Dapper;
using Domain.DataAccess.Location;
using Domain.DataModel;
using Domain.Dto;
using System.Data;
using Z.Dapper.Plus;

namespace DataAccess.Repository
{
	public class LocationRepository : ILocationRepository
	{
		private readonly IDbConnection dbConnection;

		public LocationRepository(IDbConnection dbConnection)
		{
			this.dbConnection = dbConnection;
		}

		public void AddLocations(IEnumerable<Location> locations)
		{
			dbConnection.BulkInsert(locations);
		}

		public async Task<WeatherForecastDto> GetWeatherByLocationAsync(float latitude, float longitude)
		{
			var query = @"
									SELECT TOP 1
											l.Latitude,
											l.Longitude,
											l.TimeZone,
											l.CreatedAt,
											wd.Time,
											wd.Temperature,
											wd.WeatherCode,
											wd.WindSpeed,
											wd.IsDay,
											wd.Rain,
											wd.Showers,
											wd.Snowfall,
											wd.CloudCover,
											wd.Pressure,
											wdu.Time AS UnitTime,
											wdu.Temperature AS UnitTemperature,
											wdu.WindSpeed AS UnitWindSpeed,
											wdu.Rain AS UnitRain,
											wdu.Showers AS UnitShowers,
											wdu.Snowfall AS UnitSnowfall,
											wdu.CloudCover AS UnitCloudCover,
											wdu.Pressure AS UnitPressure
									FROM
											Locations l
											INNER JOIN WeatherDetails wd ON l.Guid = wd.LocationGuid
											INNER JOIN WeatherDetailUnits wdu ON wd.Guid = wdu.WeatherDetailGuid
									WHERE
											l.Latitude = @Latitude AND l.Longitude = @Longitude
									ORDER BY CreatedAt DESC";

			var result = await dbConnection.QueryAsync<WeatherForecastDto, CurrentWeatherDto, CurrentWeatherUnitsDto, WeatherForecastDto>(
					query,
					(weatherForecast, currentWeather, currentUnits) =>
					{
						weatherForecast.CurrentWeather = currentWeather;
						weatherForecast.CurrentUnits = currentUnits;
						return weatherForecast;
					},
					splitOn: "Time,UnitTime",
					param: new { Latitude = latitude, Longitude = longitude });

			return result.FirstOrDefault();
		}
	}
}