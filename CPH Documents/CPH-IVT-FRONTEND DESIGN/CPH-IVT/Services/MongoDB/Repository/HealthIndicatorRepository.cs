using CPH_IVT.Models;
using CPH_IVT.Services.MongoDB.Context;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CPH_IVT.Services.MongoDB.Repository
{
    /// <summary>
    /// Represents a realization of <see cref="IHealthIndicatorRepository"/>.
    /// </summary>
    public sealed class HealthIndicatorRepository : IHealthIndicatorRepository
    {
        /// <summary>
        /// <see cref="IHealthIndicatorContext"/>
        /// </summary>
        private readonly IHealthIndicatorContext _context;

        /// <summary>
        /// Parameterized constructor.
        /// </summary>
        /// <param name="context"><see cref="IHealthIndicatorContext"/></param>
        public HealthIndicatorRepository(IHealthIndicatorContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Asynchronous creation of a <see cref="HealthIndicator"/> object.
        /// </summary>
        /// <param name="healthIndicator"><see cref="HealthIndicator"/></param>
        public async Task CreateAsync(HealthIndicator healthIndicator)
        {
            await _context.HealthIndicators.InsertOneAsync(healthIndicator);
        }

        public async Task CreateBulkAsync(ICollection<HealthIndicator> healthIndicators)
        {
            await _context.HealthIndicators.InsertManyAsync(healthIndicators);
        }

        /// <summary>
        /// Asynchronous retrieval of all <see cref="HealthIndicator"/> objects from a MongoDB database.
        /// </summary>
        /// <returns>A collection of <see cref="HealthIndicator"/> objects</returns>
        public async Task<ICollection<HealthIndicator>> GetAllAsync()
        {
            return await _context.HealthIndicators.Find(_ => true).ToListAsync();
        }

        /// <summary>
        /// Asynchronous retrieval of a <see cref="HealthIndicator"/> object with <see cref="HealthIndicator.Id"/> matching <paramref name="id"/>.
        /// </summary>
        /// <param name="id"><see cref="ObjectId"/></param>
        /// <returns>A <see cref="HealthIndicator"/> object</returns>
        public async Task<HealthIndicator> GetByIdAsync(ObjectId id)
        {
            var filter = Builders<HealthIndicator>.Filter.Eq(m => m.Id, id);
            return await _context.HealthIndicators.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<ICollection<HealthIndicator>> GetAllByCountyIdAsync(string id)
        {
            var filter = Builders<HealthIndicator>.Filter.Eq(m => m.CountyId, id);
            return await _context.HealthIndicators.Find(filter).ToListAsync();
        }

        /// <summary>
        /// Asynchronous update of a <see cref="HealthIndicator"/> object.
        /// </summary>
        /// <param name="healthIndicator"><see cref="HealthIndicator"/></param>
        /// <returns><see langword="true"/> if successful; <see langword="false"/> otherwise</returns>
        public async Task<bool> UpdateAsync(HealthIndicator healthIndicator)
        {
            var updateResult = await _context.HealthIndicators.ReplaceOneAsync(
                filter: indicator => indicator.Id == healthIndicator.Id,
                replacement: healthIndicator);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        /// <summary>
        /// Asynchronous hard deletion of a <see cref="HealthIndicator"/> object with <see cref="HealthIndicator.Id"/> matching <paramref name="id"/>.
        /// </summary>
        /// <param name="id"><see cref="ObjectId"/></param>
        /// <returns><see langword="true"/> if successful; <see langword="false"/> otherwise</returns>
        public async Task<bool> DeleteAsync(ObjectId id)
        {
            var filter = Builders<HealthIndicator>.Filter.Eq(healthIndicator => healthIndicator.Id, id);
            var deleteResult = await _context.HealthIndicators.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
    }
}
