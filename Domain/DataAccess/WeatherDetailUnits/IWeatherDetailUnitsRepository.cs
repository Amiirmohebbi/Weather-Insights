namespace Domain.DataAccess.WeatherDetailUnits
{
	public interface IWeatherDetailUnitsRepository : IWeatherDetailUnitsReaderRepository
	{
		void AddWeatherDetailUnits(IEnumerable<DataModel.WeatherDetailUnits> weatherDataUnits);
	}
}
