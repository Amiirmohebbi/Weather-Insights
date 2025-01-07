namespace Domain.DataAccess.WeatherDetail
{
	public interface IWeatherDetailRepository : IWeatherDetailReaderRepository
	{
		void AddWeatherDetails(IEnumerable<DataModel.WeatherDetail> weatherDetails);
	}
}
