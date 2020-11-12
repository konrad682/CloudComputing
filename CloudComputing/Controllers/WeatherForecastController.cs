using System;
using System.Collections.Generic;
using System.Linq;
using CloudComputing.Data.Entity;
using CloudComputing.Services;
using CloudComputing.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CloudComputing.Web.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeatherForecastController : ControllerBase
	{
		private static readonly string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};
		private readonly ITemperatureService _temperatureService;

		private readonly ILogger<WeatherForecastController> _logger;

		public WeatherForecastController(ILogger<WeatherForecastController> logger, ITemperatureService temperatureService)
		{
			_logger = logger;
			_temperatureService = temperatureService;
		}

		[HttpGet("all")]
		public ActionResult<List<TemperatureModel>> GetAll() =>
			_temperatureService.Get();

		[HttpGet]
		public IEnumerable<WeatherForecast> Get()
		{
			var rng = new Random();
			return Enumerable.Range(1, 5).Select(index => new WeatherForecast
			{
				Date = DateTime.Now.AddDays(index),
				TemperatureC = rng.Next(-20, 55),
				Summary = Summaries[rng.Next(Summaries.Length)]
			})
			.ToArray();
		}
	}
}
