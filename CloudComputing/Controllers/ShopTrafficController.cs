using System;
using System.Collections.Generic;
using System.Linq;
using CloudComputing.Data.Entity;
using CloudComputing.Data.Models;
using CloudComputing.Services;
using CloudComputing.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CloudComputing.Web.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ShopTrafficController : ControllerBase
	{
		private static readonly string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};
		private readonly IShopTrafficService _temperatureService;

		private readonly ILogger<ShopTrafficController> _logger;

		public ShopTrafficController(ILogger<ShopTrafficController> logger, IShopTrafficService temperatureService)
		{
			_logger = logger;
			_temperatureService = temperatureService;
		}

		[HttpPost("all")]
		public ActionResult<List<ShopTrafficModel>> GetShopTrafficResult([FromBody] PeriodOfTimeModel periodOfTime)
		{
			return _temperatureService.GetShopTrafficResult(periodOfTime.Option, periodOfTime.OptionShop);
		}

		[HttpPost("chart")]
		public ActionResult<List<ResponseChartModel>> GetShopTrafficChartResult([FromBody] PeriodOfTimeChartModel periodOfTimeChart)
		{
		
			var result = _temperatureService.GetShopTrafficChartResult(periodOfTimeChart);
			return result;
		}

		[HttpGet("getLastRecords")]
		public ActionResult<List<ShopTrafficModel>> GetShopTrafficLatestChart()
		{

			var result = _temperatureService.GetShopTrafficLatestResult();
			return result;
		}
	}
}
