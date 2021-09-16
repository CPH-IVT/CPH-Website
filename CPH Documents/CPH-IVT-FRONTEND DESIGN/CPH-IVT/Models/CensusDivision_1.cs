using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace CPH_IVT.Models
{
    public class CensusDivision
    {
        [BsonRepresentation(BsonType.Binary)]
        public int DivisionNumber { get; set; }

        [BsonRepresentation(BsonType.String)]
        public string Name { get; set; }

        [BsonRepresentation(BsonType.Document)]
        public ICollection<State> States { get; set; }
    }
}
