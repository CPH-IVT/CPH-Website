using CPH_IVT.Models;
using CPH_IVT.Services.MongoDB.Context;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CPH_IVT.Services.MongoDB.Repository
{
    /// <summary>
    /// Represents a realization of <see cref="ICountyRepository"/>.
    /// </summary>
    public sealed class CountyRepository : ICountyRepository
    {
        /// <summary>
        /// <see cref="ICountyContext"/>
        /// </summary>
        private readonly ICountyContext _context;

        /// <summary>
        /// Parameterized constructor.
        /// </summary>
        /// <param name="context"><see cref="ICountyContext"/></param>
        public CountyRepository(ICountyContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Asynchronous creation of a <see cref="County"/> object.
        /// </summary>
        /// <param name="county"><see cref="County"/></param>
        public async Task CreateAsync(County county)
        {
            await _context.Counties.InsertOneAsync(county);
        }

        public async Task CreateBulkAsync(ICollection<County> counties)
        {
            await _context.Counties.InsertManyAsync(counties);
        }

        /// <summary>
        /// Asynchronous retrieval of all <see cref="County"/> objects from a MongoDB database.
        /// </summary>
        /// <returns>A collection of <see cref="County"/> objects</returns>
        public async Task<ICollection<County>> GetAllAsync()
        {
            return await _context.Counties.Find(_ => true).ToListAsync();
        }

        /// <summary>
        /// Asynchronous retrieval of all <see cref="County"/> objects with <see cref="County.CountyId"/> matching <paramref name="countyId"/>.
        /// </summary>
        /// <param name="countyId"><see cref="County.CountyId"/></param>
        /// <returns>A collection of <see cref="County"/> objects</returns>
        public async Task<County> GetByCountyIdAsync(string countyId)
        {
            var filter = Builders<County>.Filter.Eq(county => county.CountyId, countyId);
            return await _context.Counties.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<ICollection<HealthIndicator>> GetAllHealthIndicatorsByCountyIdAsync(string countyId)
        {
            var county = await GetByCountyIdAsync(countyId);
            return county.Indicators; ;
        }

        public async Task<ICollection<County>> GetAllCountiesByStateFIPSAsync(string stateFips)
        {
            var filter = Builders<County>.Filter.Eq(county => county.StateFIPS, stateFips);
            return await _context.Counties.Find(filter).ToListAsync();
        }

        /// <summary>
        /// Asynchronous update of a <see cref="County"/> object.
        /// </summary>
        /// <param name="county"><see cref="County"/></param>
        /// <returns><see langword="true"/> if successful; <see langword="false"/> otherwise</returns>
        public async Task<bool> UpdateAsync(County county)
        {
            var updateResult = await _context.Counties.ReplaceOneAsync(
                filter: c => c.CountyId == county.CountyId,
                replacement: county);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        /// <summary>
        /// Asynchronous hard deletion of a <see cref="County"/> object with <see cref="County.CountyId"/> matching <paramref name="countyId"/>.
        /// </summary>
        /// <param name="countyId"><see cref="County.CountyId"/></param>
        /// <returns><see langword="true"/> if successful; <see langword="false"/> otherwise</returns>
        public async Task<bool> DeleteAsync(string countyId)
        {
            var filter = Builders<County>.Filter.Eq(county => county.CountyId, countyId);
            var deleteResult = await _context.Counties.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
    }
}
