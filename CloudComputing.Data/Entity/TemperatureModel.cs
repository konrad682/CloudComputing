using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CloudComputing.Data.Entity
{
	public class TemperatureModel
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }

		public string sensorId { get; set; }

		public string date { get; set; }

		public double temp { get; set; }
	}
}
