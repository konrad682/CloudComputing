using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using CloudComputing.Data;
using CloudComputing.Data.Entity;
using CloudComputing.Data.Models;
using CloudComputing.Services.Interfaces;
using MongoDB.Driver;

namespace CloudComputing.Services
{
	public class TemperatureService : ITemperatureService
	{
		private readonly IMongoCollection<ShopTrafficModel> _shopTraffic;

		public TemperatureService(IShopTrafficDatabaseSettings settings)
		{
			var client = new MongoClient(settings.ConnectionString);
			var database = client.GetDatabase(settings.DatabaseName);

			_shopTraffic = database.GetCollection<ShopTrafficModel>(settings.TemperatureCollectionName);
		}

		public List<ShopTrafficModel> GetShopTrafficResult(int periodOfTimeOption)
		{
			DateTime startDate;
			switch (periodOfTimeOption)
			{
				case (int)PeriodOfTime.LastMinute:
					startDate = DateTime.Now.AddHours(1).AddMinutes(-1);
					break;
				case (int)PeriodOfTime.LastHours:
					startDate = DateTime.Now;
					break;
				case (int)PeriodOfTime.LastFourHours:
					startDate = DateTime.Now.AddHours(-3);
					break;
				case (int)PeriodOfTime.LastDay:
					startDate = DateTime.Now.AddDays(-1);
					break;
				case (int)PeriodOfTime.LastWeek:
					startDate = DateTime.Now.AddDays(-7);
					break;
				default:
					startDate = DateTime.Now;
					break;
			}

			var shopTrafficList =_shopTraffic.Find(k => k.date > startDate).ToList();

			return shopTrafficList;
		}

		public List<ResponseChartModel> GetShopTrafficChartResult(PeriodOfTimeChartModel periodOfTimeChartModel)
		{
			DateTime tempDate = periodOfTimeChartModel.startDate;
			List<ResponseChartModel> responseChart = new List<ResponseChartModel>();
			while (tempDate <= periodOfTimeChartModel.endDate)
			{
				responseChart.Add(GetValuesInPeriodOfTime(tempDate));
				tempDate = tempDate.AddHours(1);
			}
			return responseChart;
		}

		private ResponseChartModel GetValuesInPeriodOfTime(DateTime tempDate)
		{
			var shopTrafficList = _shopTraffic.Find(k => k.date > tempDate && k.date <= tempDate.AddHours(1)).ToList();

			int avg = 0;

			foreach (var shopTraffic in shopTrafficList)
			{
				avg += shopTraffic.peopleActual;
			}

			if(avg != 0)
				avg = avg / shopTrafficList.Count;

			return new ResponseChartModel
			{
				AvgPeople = avg,
				Name = $"{tempDate.ToString()} - {tempDate.AddHours(1).ToString()}"
			};
		}
	}
}
