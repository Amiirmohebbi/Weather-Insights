using Domain.Dto;
using Domain.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Service
{
	public class WeatherForecastBackgroundWorker : BackgroundService
	{
		private readonly ILogger<WeatherForecastBackgroundWorker> logger;
		private readonly IServiceScopeFactory serviceScopeFactory;
		private readonly IWeatherForecastQueueService weatherForecastQueueService;

		public WeatherForecastBackgroundWorker(ILogger<WeatherForecastBackgroundWorker> logger, IServiceScopeFactory serviceScopeFactory, IWeatherForecastQueueService weatherForecastQueueService)
		{
			this.logger = logger;
			this.serviceScopeFactory = serviceScopeFactory;
			this.weatherForecastQueueService = weatherForecastQueueService;
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			while (!stoppingToken.IsCancellationRequested)
			{
				using (var scope = serviceScopeFactory.CreateScope())
				{
					var weatherManagerService = scope.ServiceProvider.GetRequiredService<IWeatherManagerService>();

					var batch = new List<WeatherForecastDto>();

					if (weatherForecastQueueService.GetBuffer().TryDequeue(out var data))
					{
						batch.Add(data);

						while (weatherForecastQueueService.GetBuffer().TryDequeue(out var nextData))
						{
							batch.Add(nextData);
						}
					}

					if (batch.Any())
					{
						try
						{
							weatherManagerService.ManageWeatherData(batch);
							logger.LogInformation($"{batch.Count} records inserted into the database.");
						}
						catch (Exception ex)
						{
							logger.LogError(ex, "Error during bulk insert.");
						}
					}
				}

				await Task.Delay(TimeSpan.FromSeconds(15), stoppingToken);
			}
		}

	}
}
