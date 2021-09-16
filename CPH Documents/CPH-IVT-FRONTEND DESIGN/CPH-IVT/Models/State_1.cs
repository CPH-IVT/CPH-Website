using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace CPH_IVT.Models
{
    public class State
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string FIPS { get; set; }

        [BsonRepresentation(BsonType.String)]
        public string Name { get; set; }

        [BsonRepresentation(BsonType.String)]
        public string Abbreviation { get; set; }

        [BsonRepresentation(BsonType.Document)]
        public ICollection<County> Counties { get; set; }
    }
}
