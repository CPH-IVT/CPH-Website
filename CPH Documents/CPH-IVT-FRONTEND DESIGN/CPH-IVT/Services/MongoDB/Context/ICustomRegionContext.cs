using CPH_IVT.Models;
using MongoDB.Driver;

namespace CPH_IVT.Services.MongoDB.Context
{
    /// TODO: Complete implementation
    /// <summary>
    /// Provides a mechanism for NoSQL communication with <see cref="CustomRegion"/> documents.
    /// </summary>
    public interface ICustomRegionContext
    {
        /// <summary>
        /// MongoDB-specific collection of <see cref="CustomRegion"/> documents.
        /// </summary>
        /// <seealso cref="IMongoCollection{TDocument}"/>
        IMongoCollection<CustomRegion> CustomRegions { get; }
    }
}