using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CloudComputing.Data.Entity
{
	class TemperatureModel
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; }

		public string Name { get; set; }

		public string Address { get; set; }

		public string Phone { get; set; }

		public string Email { get; set; }
    }
}
