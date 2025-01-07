using Domain.DataAccess.WeatherDetailUnits;
using Domain.DataModel;
using System.Data;
using Z.Dapper.Plus;

public class WeatherDetailUnitsRepository : IWeatherDetailUnitsRepository
{
	private readonly IDbConnection dbConnection;

	public WeatherDetailUnitsRepository(IDbConnection dbConnection)
	{
		this.dbConnection = dbConnection;
	}

	public void AddWeatherDetailUnits(IEnumerable<WeatherDetailUnits> weatherDetailUnits)
	{
		dbConnection.BulkInsert(weatherDetailUnits);
	}
}
