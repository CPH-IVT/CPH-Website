using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace CPH_IVT.Models
{
    public class County
    {
        [BsonRepresentation(BsonType.Binary)]
        public int FIPS { get; set; }

        [BsonRepresentation(BsonType.String)]
        public string Name { get; set; }

        [BsonRepresentation(BsonType.Document)]
        public ICollection<HealthIndicator> Indicators { get; set; }
    }
}