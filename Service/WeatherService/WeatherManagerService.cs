using Domain.DataAccess.Location;
using Domain.DataAccess.WeatherDetail;
using Domain.DataAccess.WeatherDetailUnits;
using Domain.DataModel;
using Domain.Dto;
using Domain.ExternalService;
using Domain.Mapping;
using Domain.Service;
using System.Transactions;

namespace Service.WeatherService
{
	public class WeatherManagerService : IWeatherManagerService
	{
		private readonly IWeatherDetailUnitsRepository weatherDetailUnitsRepository;
		private readonly IWeatherDetailRepository weatherDetailRepository;
		private readonly ILocationRepository locationRepository;
		private readonly IOpenMeteoService openMeteoService;
		
		public WeatherManagerService(IWeatherDetailUnitsRepository weatherDetailUnitsRepository,
																 IWeatherDetailRepository weatherDetailRepository,
																 ILocationRepository locationRepository,
																 IOpenMeteoService openMeteoService)
		{
			this.locationRepository = locationRepository;
			this.weatherDetailUnitsRepository = weatherDetailUnitsRepository;
			this.weatherDetailRepository = weatherDetailRepository;
			this.openMeteoService = openMeteoService;
		}

		public Task<WeatherForecastDto> GetLastWeatherAsyncByLocation(float latitude, float longitude)
		{
			return locationRepository.GetWeatherByLocationAsync(latitude, longitude);
		}

		public async Task<WeatherForecastDto> GetWeatherAsync(WeatherFetcherDto weatherFetcherDto)
		{
			var result = await openMeteoService.GetWeatherByLocationAsync(weatherFetcherDto.Latitude, weatherFetcherDto.Longitude);

			ManageWeatherDataAsync(result, weatherFetcherDto.UserId);

			return result;
		}

		private void ManageWeatherDataAsync(WeatherForecastDto weatherForecastDto, Guid userId)
		{
			var location = weatherForecastDto.ToLocationDataModel(userId);

			var weatherDetail = weatherForecastDto.CurrentWeather.ToDataModel();

			var weatherDetailUnits = weatherForecastDto.CurrentUnits.ToDataModel();

			using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
			{
				IsolationLevel = IsolationLevel.ReadCommitted,
				Timeout = TransactionManager.DefaultTimeout
			}))
			{
				var locationId = locationRepository.AddLocation(location);

				weatherDetail.LocationId = locationId;
				
				var weatherDetailId = weatherDetailRepository.AddWeatherDetail(weatherDetail);
				
				weatherDetailUnits.WeatherDetailId = weatherDetailId;

				weatherDetailUnitsRepository.AddWeatherDetailUnits(weatherDetailUnits);

				transaction.Complete();
			}
		}
	}
}
