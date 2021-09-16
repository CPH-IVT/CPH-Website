using CPH_IVT.Models;
using CPH_IVT.Services.MongoDB.Context;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CPH_IVT.Services.MongoDB.Repository
{
    /// <summary>
    /// Represents a realization of <see cref="ICensusRegionRepository"/>.
    /// </summary>
    public sealed class CensusRegionRepository : ICensusRegionRepository
    {
        /// <summary>
        /// <see cref="ICensusRegionContext"/>
        /// </summary>
        private readonly ICensusRegionContext _context;

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="context"><see cref="ICensusRegionContext"/></param>
        public CensusRegionRepository(ICensusRegionContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Asynchronous creation of a <see cref="CensusRegion"/> object.
        /// </summary>
        /// <param name="censusRegion"><see cref="CensusRegion"/></param>
        public async Task CreateAsync(CensusRegion censusRegion)
        {
            await _context.CensusRegions.InsertOneAsync(censusRegion);
        }

        public async Task CreateBulkAsync(ICollection<CensusRegion> censusRegions)
        {
            await _context.CensusRegions.InsertManyAsync(censusRegions);
        }

        /// <summary>
        /// Asynchronous retrieval of all <see cref="CensusRegion"/> objects from a MongoDB database.
        /// </summary>
        /// <returns>A collection of <see cref="CensusRegion"/> objects</returns>
        public async Task<ICollection<CensusRegion>> GetAllAsync()
        {
            return await _context.CensusRegions.Find(_ => true).ToListAsync();
        }

        /// <summary>
        /// Asynchronous retrieval of a <see cref="CensusRegion"/> object with <see cref="CensusRegion.Number"/> matching <paramref name="regionNumber"/>.
        /// </summary>
        /// <param name="regionNumber"><see cref="CensusRegion.Number"/></param>
        /// <returns>A <see cref="CensusRegion"/> object</returns>
        public async Task<CensusRegion> GetByRegionNumberAsync(string regionNumber)
        {
            var filter = Builders<CensusRegion>.Filter.Eq(region => region.Number, regionNumber);
            return await _context.CensusRegions.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<ICollection<CensusDivision>> GetAllCensusDivisionsByRegionNumberAsync(string regionNumber)
        {
            var region = await GetByRegionNumberAsync(regionNumber);
            return region.CensusDivisions;
        }

        public async Task<ICollection<State>> GetAllStatesByRegionNumberAsync(string regionNumber)
        {
            var divisions = await GetAllCensusDivisionsByRegionNumberAsync(regionNumber);
            var states = new List<State>();

            foreach (var division in divisions)
                states.AddRange(division.States);

            return states;
        }

        public async Task<ICollection<County>> GetAllCountiesByRegionNumberAsync(string regionNumber)
        {
            var states = await GetAllStatesByRegionNumberAsync(regionNumber);
            var counties = new List<County>();

            foreach (var state in states)
                counties.AddRange(state.Counties);

            return counties;
        }

        public async Task<ICollection<HealthIndicator>> GetAllHealthIndicatorsByRegionNumberAsync(string regionNumber)
        {
            var counties = await GetAllCountiesByRegionNumberAsync(regionNumber);
            var healthIndicators = new List<HealthIndicator>();

            foreach (var county in counties)
                healthIndicators.AddRange(county.Indicators);

            return healthIndicators;
        }

        /// <summary>
        /// Asynchronous update of a <see cref="CensusRegion"/> object.
        /// </summary>
        /// <param name="censusRegion"><see cref="CensusRegion"/></param>
        /// <returns><see langword="true"/> if successful; <see langword="false"/> otherwise</returns>
        public async Task<bool> UpdateAsync(CensusRegion censusRegion)
        {
            var updateResult = await _context.CensusRegions.ReplaceOneAsync(
                filter: region => region.Number == censusRegion.Number,
                replacement: censusRegion);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        /// <summary>
        /// Asynchronous hard deletion of a <see cref="CensusRegion"/> object with <see cref="CensusRegion.Number"/> matching <paramref name="regionNumber"/>.
        /// </summary>
        /// <param name="regionNumber"><see cref="CensusRegion.Number"/></param>
        /// <returns><see langword="true"/> if successful; <see langword="false"/> otherwise</returns>
        public async Task<bool> DeleteAsync(string regionNumber)
        {
            var filter = Builders<CensusRegion>.Filter.Eq(censusRegion => censusRegion.Number, regionNumber);
            var deleteResult = await _context.CensusRegions.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
    }
}
