using System;
using System.Collections.Generic;
using System.Text;
using CloudComputing.Data.Entity;
using CloudComputing.Services.Interfaces;
using MongoDB.Driver;

namespace CloudComputing.Services
{
	public class TemperatureService : ITemperatureService
	{
		private readonly IMongoCollection<TemperatureModel> _books;

		public TemperatureService(IShopTrafficDatabaseSettings settings)
		{
			var client = new MongoClient(settings.ConnectionString);
			var database = client.GetDatabase(settings.DatabaseName);

			_books = database.GetCollection<TemperatureModel>(settings.TemperatureCollectionName);
		}

		public List<TemperatureModel> Get() =>
			_books.Find(book => true).ToList();

		public TemperatureModel Get(string id) =>
			_books.Find<TemperatureModel>(book => book.Id == id).FirstOrDefault();

		public TemperatureModel Create(TemperatureModel book)
		{
			_books.InsertOne(book);
			return book;
		}

		public void Update(string id, TemperatureModel bookIn) =>
			_books.ReplaceOne(book => book.Id == id, bookIn);

		public void Remove(TemperatureModel bookIn) =>
			_books.DeleteOne(book => book.Id == bookIn.Id);

		public void Remove(string id) =>
			_books.DeleteOne(book => book.Id == id);
	}
}
