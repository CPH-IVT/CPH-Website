using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace CPH_IVT.Models
{
    /// <summary>
    /// Represents one of nine U.S. Census Bureau-defined or one developer-defined ('Territories')
    /// geographic divisions.
    /// </summary>

    [BsonIgnoreExtraElements]
    public class CensusDivision
    {
        /// <summary>
        /// Government-issued division identifier.
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Full name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Collection of U.S. states or territories
        /// </summary>
        /// <seealso cref="State"/>
        public ICollection<State> States { get; set; }

        ///// <summary>
        ///// <see cref="CensusRegion.Number"/>
        ///// </summary>
        public string CensusRegionNumber { get; set; }
    }
}
