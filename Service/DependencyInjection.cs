using Domain.ExternalService;
using Domain.Service;
using Microsoft.Extensions.DependencyInjection;
using Service.ExternalService;
using Service.WeatherService;

namespace Service
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddServiceServices(this IServiceCollection services)
		{
			services.AddScoped<IOpenMeteoService, OpenMeteoService>();
			services.AddHostedService<WeatherForecastBackgroundWorker>();
			services.AddScoped<IWeatherManagerService, WeatherManagerService>();
			services.AddSingleton<IWeatherForecastQueueService, WeatherForecastQueueService>();

			return services;
		}
	}
}
