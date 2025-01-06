namespace Domain.DataAccess.WeatherDetailUnits
{
	public interface IWeatherDetailUnitsRepository : IWeatherDetailUnitsReaderRepository
	{
		int AddWeatherDetailUnits(DataModel.WeatherDetailUnits weatherDataUnits);
	}
}
