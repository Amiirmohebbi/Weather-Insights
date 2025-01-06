using Domain.Dto;
using Domain.ExternalService;
using OpenMeteo;
using Service.Mapping;

namespace Service.ExternalService
{
	public class OpenMeteoService : IOpenMeteoService
	{
		private readonly OpenMeteoClient openMeteoClient;

		public OpenMeteoService() =>
			openMeteoClient = new OpenMeteoClient();


		public async Task<WeatherForecastDto> GetWeatherByLocationAsync(float latitude, float longitude)
		{
			var weatherData = await openMeteoClient.QueryAsync(new WeatherForecastOptions
			{
				Latitude = latitude,
				Longitude = longitude,
				Current = CurrentOptions.All
			}) ?? throw new Exception("an error accured.");

			return weatherData.ToDto();
		}
	}
}
