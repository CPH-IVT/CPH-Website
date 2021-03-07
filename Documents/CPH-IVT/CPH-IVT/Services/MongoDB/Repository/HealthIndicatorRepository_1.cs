using CPH_IVT.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPH_IVT.Services.MongoDB.Repository
{
    public class HealthIndicatorRepository : IHealthIndicatorRepository
    {
        private readonly IHealthIndicatorContext _context;

        public HealthIndicatorRepository(IHealthIndicatorContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<HealthIndicator>> GetAllHealthIndicators()
        {
            return await _context
                            .HealthIndicators
                            .Find(_ => true)
                            .ToListAsync();
        }

        public Task<HealthIndicator> GetHealthIndicator(string name)
        {
            FilterDefinition<HealthIndicator> filter = Builders<HealthIndicator>.Filter.Eq(m => m.Name, name);
            return _context
                    .HealthIndicators
                    .Find(filter)
                    .FirstOrDefaultAsync();
        }

        public async Task CreateHealthIndicator(HealthIndicator healthIndicator)
        {
            await _context.HealthIndicators.InsertOneAsync(healthIndicator);
        }

        public async Task<bool> DeleteHealthIndicator(string name)
        {
            FilterDefinition<HealthIndicator> filter = Builders<HealthIndicator>.Filter.Eq(m => m.Name, name);
            DeleteResult deleteResult = await _context
                                                .HealthIndicators
                                                .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }

    }
}
