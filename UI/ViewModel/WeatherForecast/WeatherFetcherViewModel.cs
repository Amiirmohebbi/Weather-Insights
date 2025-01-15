using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace UI.ViewModel.WeatherForecast
{
	public class WeatherFetcherViewModel
	{
		[FromHeader]
		public Guid UserId { get; set; }

		[FromQuery]
		[Range(-90, 90, ErrorMessage = "Latitude must be in range of -90 to 90°.")]
		public float Latitude { get; set; }

		[FromQuery]
		[Range(-180, 180, ErrorMessage = "Longitude must be in range of -180 to 180°.")]
		public float Longitude { get; set; }
	}
}
