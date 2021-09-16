using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace CPH_IVT.Models
{
    /// <summary>
    /// Represents one of four U.S. Census Bureau-defined or one developer-defined ('Territories')
    /// geographic regions.
    /// </summary>

    [BsonIgnoreExtraElements]
    public class CensusRegion
    {
        /// <summary>
        /// Government-issued region identifier.
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Full name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Collection of U.S. Census divisions.
        /// </summary>
        /// <seealso cref="CensusDivision"/>
        public ICollection<CensusDivision> CensusDivisions { get; set; }
    }
}
