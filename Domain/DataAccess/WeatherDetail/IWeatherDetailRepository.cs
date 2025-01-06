namespace Domain.DataAccess.WeatherDetail
{
	public interface IWeatherDetailRepository : IWeatherDetailReaderRepository
	{
		int AddWeatherDetail(DataModel.WeatherDetail weatherDetail);
	}
}
