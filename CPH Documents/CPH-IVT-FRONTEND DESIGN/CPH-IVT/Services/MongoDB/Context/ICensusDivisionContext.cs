using CPH_IVT.Models;
using MongoDB.Driver;

namespace CPH_IVT.Services.MongoDB.Context
{
    /// <summary>
    /// Provides a mechanism for NoSQL communication with <see cref="CensusDivision"/> documents.
    /// </summary>
    public interface ICensusDivisionContext
    {
        /// <summary>
        /// MongoDB-specific collection of <see cref="CensusDivision"/> documents.
        /// </summary>
        /// <seealso cref="IMongoCollection{TDocument}"/>
        IMongoCollection<CensusDivision> CensusDivisions { get; }
    }
}