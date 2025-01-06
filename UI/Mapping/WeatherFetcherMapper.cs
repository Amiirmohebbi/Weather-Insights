using Domain.Dto;
using UI.ViewModel.WeatherForecast;

namespace UI.Mapping
{
	public static class WeatherFetcherMapper
	{
		public static WeatherFetcherDto ToDto(this WeatherFetcherViewModel model) 
		{
			return new WeatherFetcherDto() 
			{
				UserId = model.UserId,
				Latitude = model.Latitude,
				Longitude = model.Longitude
			};
		}
	}
}
