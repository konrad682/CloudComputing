using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using CloudComputing.Data.Entity;
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

		public List<ShopTrafficModel> GetAll()
		{
			DateTime startDate = DateTime.Now.AddHours(-2);

			var shopTrafficList =_shopTraffic.Find(k => k.date > startDate).ToList();

			return shopTrafficList;
		}

		public ShopTrafficModel Get(string id) =>
			_shopTraffic.Find<ShopTrafficModel>(book => book.Id == id).FirstOrDefault();

		public ShopTrafficModel Create(ShopTrafficModel book)
		{
			_shopTraffic.InsertOne(book);
			return book;
		}

		public void Update(string id, ShopTrafficModel bookIn) =>
			_shopTraffic.ReplaceOne(book => book.Id == id, bookIn);

		public void Remove(TemperatureModel bookIn) =>
			_shopTraffic.DeleteOne(book => book.Id == bookIn.Id);

		public void Remove(string id) =>
			_shopTraffic.DeleteOne(book => book.Id == id);
	}
}
