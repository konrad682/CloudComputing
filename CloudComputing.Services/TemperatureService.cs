using System;
using System.Collections.Generic;
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

			_shopTraffic = database.GetCollection<ShopTrafficModel>(settings.ShopTrafficCollectionName);
		}

		public List<ShopTrafficModel> GetShopTrafficResult(int periodOfTimeOption, string optionShopValue)
		{
			int kindOfShop = 0;
			if (optionShopValue.Equals("option1"))
			{
				kindOfShop = 1;
			}
			else if (optionShopValue.Equals("option2"))
			{
				kindOfShop = 2;
			}

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

			var shopTrafficList =_shopTraffic.Find(k => k.date > startDate && k.shop_id == kindOfShop).ToList();

			return shopTrafficList;
		}

		public List<ResponseChartModel> GetShopTrafficChartResult(PeriodOfTimeChartModel periodOfTimeChartModel)
		{
			int kindOfShop = 0;
			if (periodOfTimeChartModel.OptionShop.Equals("option1"))
			{
				kindOfShop = 1;
			}
			else if (periodOfTimeChartModel.OptionShop.Equals("option2"))
			{
				kindOfShop = 2;
			}

			DateTime tempDate = periodOfTimeChartModel.startDate;
			List<ResponseChartModel> responseChart = new List<ResponseChartModel>();
			while (tempDate <= periodOfTimeChartModel.endDate)
			{
				responseChart.Add(GetValuesInPeriodOfTime(tempDate, kindOfShop));
				tempDate = tempDate.AddHours(1);
			}
			return responseChart;
		}

		private ResponseChartModel GetValuesInPeriodOfTime(DateTime tempDate, int kindOfShop)
		{
			var shopTrafficList = _shopTraffic.Find(k => k.date > tempDate && k.date <= tempDate.AddHours(1) && k.shop_id == kindOfShop).ToList();

			int avg = 0;

			foreach (var shopTraffic in shopTrafficList)
			{
				avg += shopTraffic.current_people_quantity;
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
