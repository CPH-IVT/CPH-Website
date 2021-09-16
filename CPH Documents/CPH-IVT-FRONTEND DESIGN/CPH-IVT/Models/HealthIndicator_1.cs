using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CPH_IVT.Models
{
    public class HealthIndicator
    {
        [BsonId]
        public int Id { get; set; }

        //[BsonRepresentation(BsonType.String)]
        public string Name { get; set; }

        //[BsonRepresentation(BsonType.Binary)]
        public string Year { get; set; }
    }
}
