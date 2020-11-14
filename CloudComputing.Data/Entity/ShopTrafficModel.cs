using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CloudComputing.Data.Entity
{
	public class ShopTrafficModel
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }
		public int shop_id { get; set; }
		public int sensor_id { get; set; }
		public DateTime date { get; set; }
		public int people_entered { get; set; }
		public int people_left { get; set; }
		public int current_people_quantity { get; set; }
	}
}
