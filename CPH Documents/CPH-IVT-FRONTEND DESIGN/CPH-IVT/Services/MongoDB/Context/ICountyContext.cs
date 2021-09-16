using CPH_IVT.Models;
using MongoDB.Driver;

namespace CPH_IVT.Services.MongoDB.Context
{
    /// <summary>
    /// Provides a mechanism for NoSQL communication with <see cref="County"/> documents.
    /// </summary>
    public interface ICountyContext
    {
        /// <summary>
        /// MongoDB-specific collection of <see cref="County"/> documents.
        /// </summary>
        /// <seealso cref="IMongoCollection{TDocument}"/>
        IMongoCollection<County> Counties { get; }
    }
}