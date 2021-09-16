using CPH_IVT.Models;
using MongoDB.Driver;

namespace CPH_IVT.Services.MongoDB.Context
{
    /// <summary>
    /// Provides a mechanism for NoSQL communication with <see cref="HealthIndicator"/> documents.
    /// </summary>
    public interface IHealthIndicatorContext
    {
        /// <summary>
        /// MongoDB-specific collection of <see cref="HealthIndicator"/> documents.
        /// </summary>
        /// <seealso cref="IMongoCollection{TDocument}"/>
        IMongoCollection<HealthIndicator> HealthIndicators { get; }
    }
}