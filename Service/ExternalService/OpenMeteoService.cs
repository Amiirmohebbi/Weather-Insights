using Domain.Dto;
using Domain.ExternalService;
using Domain.Service;
using OpenMeteo;
using Service.Mapping;

namespace Service.ExternalService
{
	public class OpenMeteoService : IOpenMeteoService
	{
		private readonly OpenMeteoClient openMeteoClient;
		private readonly IWeatherForecastQueueService weatherForecastQueueService;

		public OpenMeteoService(IWeatherForecastQueueService weatherForecastQueueService)
		{
			openMeteoClient = new OpenMeteoClient();
			this.weatherForecastQueueService = weatherForecastQueueService;
		}

		public async Task<WeatherForecastDto> GetWeatherByLocationAsync(WeatherFetcherDto weatherFetcherDto)
		{
			var weatherData = await openMeteoClient.QueryAsync(new WeatherForecastOptions
			{
				Latitude = weatherFetcherDto.Latitude,
				Longitude = weatherFetcherDto.Longitude,
				Current = CurrentOptions.All
			}) ?? throw new Exception("an error accured.");

			var result = weatherData.ToDto(weatherFetcherDto.UserId);


			weatherForecastQueueService.AddToBuffer(result);

			return result;
		}
	}
}
