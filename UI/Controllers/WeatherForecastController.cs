using Domain.Service;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using UI.Mapping;
using UI.ViewModel.WeatherForecast;

namespace UI.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController : ControllerBase
	{
		private readonly IWeatherManagerService weatherManagerService;
		private readonly ILogger<WeatherForecastController> logger;

		public WeatherForecastController(IWeatherManagerService weatherManagerService, ILogger<WeatherForecastController> logger)
		{
			this.weatherManagerService = weatherManagerService;
			this.logger = logger;
		}

		[HttpGet(Name = "GetWeatherForecast")]
		public async Task<IActionResult> Get(WeatherFetcherViewModel weatherFetcherModel)
		{
			try
			{
				var result = await weatherManagerService.GetWeatherAsync(weatherFetcherModel.ToDto());
				return Ok(result.ToViewModel());
			}
			catch (Exception ex)
			{
				logger.LogError(ex, "An error occurred while fetching weather data for {@WeatherFetcherModel}", weatherFetcherModel);

				var result = await weatherManagerService.GetLastWeatherByLocationAsync(weatherFetcherModel.Latitude, weatherFetcherModel.Longitude);

				if (result != null) 
				{
					return StatusCode(
						StatusCodes.Status500InternalServerError,
						new
						{
							statusCode = 500,
							message = "Failed to fetch real time weather data. This is the last known data for your given location.",
							data = result.ToViewModel()
						});
				}

				return StatusCode(StatusCodes.Status500InternalServerError);
			}
		}
	}
}
