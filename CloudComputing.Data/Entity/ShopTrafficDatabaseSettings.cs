using System;
using System.Collections.Generic;
using System.Text;

namespace CloudComputing.Data.Entity
{
	public class ShopTrafficDatabaseSettings : IShopTrafficDatabaseSettings
	{
		public string ShopTrafficCollectionName { get; set; }
		public string ConnectionString { get; set; }
		public string DatabaseName { get; set; }
	}

	public interface IShopTrafficDatabaseSettings
	{
		string ShopTrafficCollectionName { get; set; }
		string ConnectionString { get; set; }
		string DatabaseName { get; set; }
	}
}

