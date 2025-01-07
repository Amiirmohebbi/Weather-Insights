using Domain.Dto;
using System.Collections.Concurrent;

namespace Domain.Service
{
	public interface IWeatherForecastQueueService
	{
		ConcurrentQueue<WeatherForecastDto> GetBuffer();
		void AddToBuffer(WeatherForecastDto data);
	}
}
