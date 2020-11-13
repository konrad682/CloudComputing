using System;
using System.Collections.Generic;
using System.Text;
using CloudComputing.Data.Entity;
using CloudComputing.Data.Models;

namespace CloudComputing.Services.Interfaces
{
	public interface ITemperatureService
	{
		public List<ShopTrafficModel> GetShopTrafficResult(int periodOfTimeOption);
		public List<ResponseChartModel> GetShopTrafficChartResult(PeriodOfTimeChartModel periodOfTimeChartModel);
	}
}
