using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace CPH_IVT.Models
{
    /// <summary>
    /// Represents a population-level indicator that describes the health and quality 
    /// of life of a geographic community, including trends, disparities, and the 
    /// ability to compare metrics with those of other communities.
    /// Refer to: https://www.ncbi.nlm.nih.gov/pmc/articles/PMC5296688/
    /// </summary>
    public class HealthIndicator
    {
        /// <summary>
        /// Database identifier.
        /// </summary>
        /// <seealso cref="ObjectId"/>
        [BsonId(IdGenerator = typeof(ObjectIdGenerator))]
        public ObjectId Id { get; set; }

        /// <summary>
        /// Indicator descriptor.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Calendar year in which the indicator is recorded.
        /// </summary>
        public string Year { get; set; }

        /// <summary>
        /// Measure of indicator relative to its descriptor.
        /// </summary>
        public double Value { get; set; }

        ///// <summary>
        ///// <see cref="CensusRegion.Number"/>
        ///// </summary>
        //public string CensusRegionNumber { get; set; }

        ///// <summary>
        ///// <see cref="CensusDivision.Number"/>
        ///// </summary>
        //public string CensusDivisionNumber { get; set; }

        ///// <summary>
        ///// <see cref="State.FIPS"/> object
        ///// </summary>
        //public string StateFIPS { get; set; }

        ///// <summary>
        ///// <see cref="County.CountyId"/>
        ///// </summary>
        public string CountyId { get; set; }
    }
}
