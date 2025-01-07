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
			var result = await openMeteoService.GetWeatherByLocationAsync(weatherFetcherDto);

			return result;
		}

		public void ManageWeatherData(IEnumerable<WeatherForecastDto> weatherForecastDtos)
		{
			var locations = new List<Location>();
			var weatherDetails = new List<WeatherDetail>();
			var weatherDetailUnits = new List<WeatherDetailUnits>();

			foreach (var weatherForecastDto in weatherForecastDtos)
			{
				var location = weatherForecastDto.ToLocationDataModel();
				var weatherDetail = weatherForecastDto.CurrentWeather.ToDataModel(location.Guid);
				var weatherDetailUnit = weatherForecastDto.CurrentUnits.ToDataModel(weatherDetail.Guid);
				
				locations.Add(location);
				weatherDetails.Add(weatherDetail);
				weatherDetailUnits.Add(weatherDetailUnit);
			}

			using (var transaction = new TransactionScope(TransactionScopeOption.Required, new TransactionOptions
			{
				IsolationLevel = IsolationLevel.ReadCommitted,
				Timeout = TransactionManager.DefaultTimeout
			}))
			{
				locationRepository.AddLocations(locations);
				weatherDetailRepository.AddWeatherDetails(weatherDetails);
				weatherDetailUnitsRepository.AddWeatherDetailUnits(weatherDetailUnits);

				transaction.Complete();
			}
		}
	}
}
