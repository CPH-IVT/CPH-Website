using CPH_IVT.Models;
using MongoDB.Driver;

namespace CPH_IVT.Services.MongoDB.Context
{
    /// <summary>
    /// Provides a mechanism for NoSQL communication with <see cref="CensusRegion"/> documents.
    /// </summary>
    public interface ICensusRegionContext
    {
        /// <summary>
        /// MongoDB-specific collection of <see cref="CensusRegions"/> documents.
        /// </summary>
        /// <seealso cref="IMongoCollection{TDocument}"/>
        IMongoCollection<CensusRegion> CensusRegions { get; }
    }
}