using Domain.DataModel;
using Domain.Dto;

namespace Domain.Mapping
{
	public static class WeatherForecastMapper
	{
		public static Location ToLocationDataModel(this WeatherForecastDto weatherForecastDto, Guid userId)
		{
			return new Location()
			{
				UserId = userId,
				Latitude = weatherForecastDto.Latitude,
				Longitude = weatherForecastDto.Longitude,
				TimeZone = weatherForecastDto.Timezone,
				CreatedAt = DateTime.Now
			};
		}
	}
}
