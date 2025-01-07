using DataAccess.Repository;
using Domain.DataAccess.Location;
using Domain.DataAccess.WeatherDetail;
using Domain.DataAccess.WeatherDetailUnits;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddDataAccessServices(this IServiceCollection services)
		{
			services.AddScoped<ILocationRepository, LocationRepository>();
			services.AddScoped<IWeatherDetailRepository, WeatherDetailRepository>();
			services.AddScoped<IWeatherDetailUnitsRepository, WeatherDetailUnitsRepository>();

			return services;
		}
	}
}
