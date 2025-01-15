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
			var timeoutTask = Task.Delay(10 * 1000);

			var weatherData = openMeteoClient.QueryAsync(new WeatherForecastOptions
			{
				Latitude = weatherFetcherDto.Latitude,
				Longitude = weatherFetcherDto.Longitude,
				Current = CurrentOptions.All
			}) ?? throw new Exception("an error accured.");

			var completedTask = await Task.WhenAny(weatherData, timeoutTask);


			if (completedTask == weatherData)
			{
				 var result = weatherData.Result.ToDto(weatherFetcherDto.UserId);
				weatherForecastQueueService.AddToBuffer(result);

				return result;
			}
			else 
			{
				throw new TimeoutException("The external service call timed out.");
			}
		}
	}
}
