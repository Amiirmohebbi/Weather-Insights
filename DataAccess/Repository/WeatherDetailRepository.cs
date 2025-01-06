using Dapper;
using Domain.DataAccess.WeatherDetail;
using Domain.DataModel;
using System.Data;

namespace DataAccess.Repository
{
	public class WeatherDetailRepository : IWeatherDetailRepository
	{
		private readonly IDbConnection dbConnection;

		public WeatherDetailRepository(IDbConnection dbConnection)
		{
			this.dbConnection = dbConnection;
		}

		public int AddWeatherDetail(WeatherDetail weatherDetail)
		{
			var sql = @"
            INSERT INTO WeatherDetails (LocationId, Time, Temperature, WeatherCode, WindSpeed, IsDay, Rain, Showers, Snowfall, CloudCover, Pressure)
            VALUES (@LocationId, @Time, @Temperature, @WeatherCode, @WindSpeed, @IsDay, @Rain, @Showers, @Snowfall, @CloudCover, @Pressure);
            SELECT CAST(SCOPE_IDENTITY() as int);";

			return dbConnection.ExecuteScalar<int>(sql, weatherDetail);
		}
	}
}