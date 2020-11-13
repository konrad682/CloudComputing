using System;
using System.Collections.Generic;
using System.Text;
using CloudComputing.Data.Entity;
using CloudComputing.Data.Models;

namespace CloudComputing.Services.Interfaces
{
	public interface IShopTrafficService
	{
		public List<ShopTrafficModel> GetShopTrafficResult(int periodOfTimeOption, string optionShopValue);
		public List<ResponseChartModel> GetShopTrafficChartResult(PeriodOfTimeChartModel periodOfTimeChartModel);
		public List<ShopTrafficModel> GetShopTrafficLatestCharts();
	}
}
