using Domain.Dto;
using Domain.Service;
using System.Collections.Concurrent;

namespace Service
{
	public class WeatherForecastQueueService : IWeatherForecastQueueService
	{
		private readonly ConcurrentQueue<WeatherForecastDto> weatherDataBuffer = new();

		public void AddToBuffer(WeatherForecastDto data)
		{
			weatherDataBuffer.Enqueue(data);
		}

		public ConcurrentQueue<WeatherForecastDto> GetBuffer() => weatherDataBuffer;
	}
}
