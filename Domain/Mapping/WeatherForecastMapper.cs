using Domain.DataModel;
using Domain.Dto;

namespace Domain.Mapping
{
	public static class WeatherForecastMapper
	{
		public static Location ToLocationDataModel(this WeatherForecastDto weatherForecastDto)
		{
			return new Location()
			{
				UserId = weatherForecastDto.UserId,
				Latitude = weatherForecastDto.Latitude,
				Longitude = weatherForecastDto.Longitude,
				TimeZone = weatherForecastDto.Timezone,
				CreatedAt = DateTime.Now
			};
		}
	}
}
