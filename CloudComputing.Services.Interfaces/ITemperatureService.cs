using System;
using System.Collections.Generic;
using System.Text;
using CloudComputing.Data.Entity;

namespace CloudComputing.Services.Interfaces
{
	public interface ITemperatureService
	{
		public List<ShopTrafficModel> GetShopTrafficResult(int periodOfTimeOption);
	}
}
