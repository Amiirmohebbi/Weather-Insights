using Domain.DataAccess.WeatherDetail;
using Domain.DataModel;
using System.Data;
using Z.Dapper.Plus;

namespace DataAccess.Repository
{
	public class WeatherDetailRepository : IWeatherDetailRepository
	{
		private readonly IDbConnection dbConnection;

		public WeatherDetailRepository(IDbConnection dbConnection)
		{
			this.dbConnection = dbConnection;
		}

		public void AddWeatherDetails(IEnumerable<WeatherDetail> weatherDetails)
		{
			dbConnection.BulkInsert(weatherDetails);
		}
	}
}