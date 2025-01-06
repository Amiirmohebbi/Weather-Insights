using Dapper;
using Domain.DataAccess.WeatherDetailUnits;
using Domain.DataModel;
using System.Data;

public class WeatherDetailUnitsRepository : IWeatherDetailUnitsRepository
{
	private readonly IDbConnection dbConnection;

	public WeatherDetailUnitsRepository(IDbConnection dbConnection)
	{
		this.dbConnection = dbConnection;
	}

	public int AddWeatherDetailUnits(WeatherDetailUnits weatherDetailUnits)
	{
		var sql = @"
            INSERT INTO WeatherDetailUnits (WeatherDetailId, Time, Temperature, WindSpeed, Rain, Showers, Snowfall, CloudCover, Pressure)
            VALUES (@WeatherDetailId, @Time, @Temperature, @WindSpeed, @Rain, @Showers, @Snowfall, @CloudCover, @Pressure);
            SELECT CAST(SCOPE_IDENTITY() as int);";

		return dbConnection.ExecuteScalar<int>(sql, weatherDetailUnits);
	}
}
