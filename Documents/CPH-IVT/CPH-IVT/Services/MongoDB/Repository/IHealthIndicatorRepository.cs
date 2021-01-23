using CPH_IVT.Models;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CPH_IVT.Services.MongoDB.Repository
{
    /// <summary>
    /// Provides a mechanism for CRUD operations on <see cref="HealthIndicator"/> objects.
    /// </summary>
    public interface IHealthIndicatorRepository
    {
        /// <summary>
        /// Asynchronous creation of a <see cref="HealthIndicator"/> object.
        /// </summary>
        /// <param name="healthIndicator"><see cref="HealthIndicator"/></param>
        Task CreateAsync(HealthIndicator healthIndicator);

        Task CreateBulkAsync(ICollection<HealthIndicator> healthIndicators);

        /// <summary>
        /// Asynchronous retrieval of all <see cref="HealthIndicator"/> objects from a MongoDB database.
        /// </summary>
        /// <returns>A collection of <see cref="HealthIndicator"/> objects</returns>
        Task<ICollection<HealthIndicator>> GetAllAsync();

        /// <summary>
        /// Asynchronous retrieval of a <see cref="HealthIndicator"/> object with <see cref="HealthIndicator.Id"/> matching <paramref name="id"/>.
        /// </summary>
        /// <param name="id"><see cref="ObjectId"/></param>
        /// <returns>A <see cref="HealthIndicator"/> object</returns>
        Task<HealthIndicator> GetByIdAsync(ObjectId id);

        /// <summary>
        /// Asynchronous update of a <see cref="HealthIndicator"/> object.
        /// </summary>
        /// <param name="healthIndicator"><see cref="HealthIndicator"/></param>
        /// <returns><see langword="true"/> if successful; <see langword="false"/> otherwise</returns>
        Task<bool> UpdateAsync(HealthIndicator healthIndicator);

        /// <summary>
        /// Asynchronous hard deletion of a <see cref="HealthIndicator"/> object with <see cref="HealthIndicator.Id"/> matching <paramref name="id"/>.
        /// </summary>
        /// <param name="id"><see cref="ObjectId"/></param>
        /// <returns><see langword="true"/> if successful; <see langword="false"/> otherwise</returns>
        Task<bool> DeleteAsync(ObjectId id);

        Task<ICollection<HealthIndicator>> GetAllByCountyIdAsync(string id);
    }
}
