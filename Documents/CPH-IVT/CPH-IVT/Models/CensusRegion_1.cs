using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace CPH_IVT.Models
{
    public class CensusRegion
    {
        [BsonRepresentation(BsonType.Binary)]
        public int RegionNumber { get; set; }

        [BsonRepresentation(BsonType.String)]
        public string Name { get; set; }

        [BsonRepresentation(BsonType.Document)]
        public ICollection<CensusDivision> CensusDivisions { get; set; }
    }
}
