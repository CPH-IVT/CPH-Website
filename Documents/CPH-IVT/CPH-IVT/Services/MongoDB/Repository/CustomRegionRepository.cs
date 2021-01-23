using CPH_IVT.Models;
using CPH_IVT.Services.MongoDB.Context;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CPH_IVT.Services.MongoDB.Repository
{
    /// TODO: Complete implementation
    /// <summary>
    /// Represents a realization of <see cref="ICustomRegionRepository"/>.
    /// </summary>
    public class CustomRegionRepository : ICustomRegionRepository
    {
        private readonly ICustomRegionContext _context;

        public CustomRegionRepository(ICustomRegionContext context)
        {
            _context = context;
        }

        public virtual async Task<ICollection<CustomRegion>> GetAllAsync()
        {
            return await _context.CustomRegions.Find(_ => true).ToListAsync();
        }

        public virtual async Task<CustomRegion> GetByRegionNumberAsync(Guid id)
        {
            FilterDefinition<CustomRegion> filter = Builders<CustomRegion>.Filter.Eq(m => m.Id, id);
            return await _context.CustomRegions.Find(filter).FirstOrDefaultAsync();
        }

        public virtual async Task<CustomRegion> GetByNameAsync(string name)
        {
            FilterDefinition<CustomRegion> filter = Builders<CustomRegion>.Filter.Eq(m => m.Name, name);
            return await _context.CustomRegions.Find(filter).FirstOrDefaultAsync();
        }

        public virtual async Task CreateAsync(CustomRegion healthDataObject)
        {
            await _context.CustomRegions.InsertOneAsync(healthDataObject);
        }

        public virtual async Task<bool> UpdateAsync(CustomRegion healthDataObject)
        {
            ReplaceOneResult updateResult = await _context.CustomRegions.ReplaceOneAsync(
                filter: g => g.Id == healthDataObject.Id,
                replacement: healthDataObject);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        public virtual async Task<bool> DeleteAsync(Guid id)
        {
            FilterDefinition<CustomRegion> filter = Builders<CustomRegion>.Filter.Eq(m => m.Id, id);
            DeleteResult deleteResult = await _context.CustomRegions.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
    }
}
