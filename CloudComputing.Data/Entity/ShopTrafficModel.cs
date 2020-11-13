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
		public string shopId { get; set; }
		[BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
		public DateTime date { get; set; }
		public int peopleIn { get; set; }
		public int peopleOut { get; set; }
		public int peopleActual { get; set; }
	}
}
