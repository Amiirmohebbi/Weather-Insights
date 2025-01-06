using Dapper;
using Domain.DataAccess.Location;
using Domain.DataModel;
using Domain.Dto;
using System.Data;

namespace DataAccess.Repository
{
	public class LocationRepository : ILocationRepository
	{
		private readonly IDbConnection dbConnection;

		public LocationRepository(IDbConnection dbConnection)
		{
			this.dbConnection = dbConnection;
		}

		public int AddLocation(Location location)
		{
			var sql = @"
            INSERT INTO Locations (UserId, Latitude, Longitude, TimeZone, CreatedAt)
            VALUES (@UserId, @Latitude, @Longitude, @TimeZone, @CreatedAt);
            SELECT CAST(SCOPE_IDENTITY() as int);";

			return dbConnection.ExecuteScalar<int>(sql, location);
		}

		public async Task<WeatherForecastDto> GetWeatherByLocationAsync(float latitude, float longitude)
		{
			var query = @"
									SELECT
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
											INNER JOIN WeatherDetails wd ON l.Id = wd.LocationId
											INNER JOIN WeatherDetailUnits wdu ON wd.Id = wdu.WeatherDetailId
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