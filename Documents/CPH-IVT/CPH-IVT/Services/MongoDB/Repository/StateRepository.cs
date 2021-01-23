using CPH_IVT.Models;
using CPH_IVT.Services.MongoDB.Context;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CPH_IVT.Services.MongoDB.Repository
{
    /// <summary>
    /// Represents a realization of <see cref="IStateRepository"/>.
    /// </summary>
    public sealed class StateRepository : IStateRepository
    {
        /// <summary>
        /// <see cref="IStateContext"/>
        /// </summary>
        private readonly IStateContext _context;

        /// <summary>
        /// Parameterized constructor.
        /// </summary>
        /// <param name="context"><see cref="IStateContext"/></param>
        public StateRepository(IStateContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Asynchronous creation of a <see cref="State"/> object.
        /// </summary>
        /// <param name="state"><see cref="State"/></param>
        public async Task CreateAsync(State state)
        {
            await _context.States.InsertOneAsync(state);
        }

        public async Task CreateBulkAsync(ICollection<State> states)
        {
            await _context.States.InsertManyAsync(states);
        }

        /// <summary>
        /// Asynchronous retrieval of all <see cref="State"/> objects from a MongoDB database.
        /// </summary>
        /// <returns>A collection of <see cref="State"/> objects</returns>
        public async Task<ICollection<State>> GetAllAsync()
        {
            return await _context.States.Find(_ => true).ToListAsync();
        }

        /// <summary>
        /// Asynchronous retrieval of a <see cref="State"/> object with <see cref="State.FIPS"/> matching <paramref name="fips"/>.
        /// </summary>
        /// <param name="fips"><see cref="State.FIPS"/></param>
        /// <returns>A <see cref="State"/> object</returns>
        public async Task<State> GetByFIPSAsync(string fips)
        {
            var filter = Builders<State>.Filter.Eq(state => state.FIPS, fips);
            return await _context.States.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<ICollection<County>> GetAllCountiesByStateFIPSAsync(string fips)
        {
            var state = await GetByFIPSAsync(fips);
            return state.Counties;
        }

        public async Task<ICollection<HealthIndicator>> GetAllHealthIndicatorsByStateFIPSAsync(string fips)
        {
            var counties = await GetAllCountiesByStateFIPSAsync(fips);
            var healthIndicators = new List<HealthIndicator>();

            foreach (var county in counties)
                healthIndicators.AddRange(county.Indicators);

            return healthIndicators;
        }

        public async Task<ICollection<State>> GetAllStatesByDivisionNumberAsync(string divisionNumber)
        {
            var filter = Builders<State>.Filter.Eq(state => state.CensusDivisionNumber, divisionNumber);
            return await _context.States.Find(filter).ToListAsync();
        }

        /// <summary>
        /// Asynchronous update of a <see cref="State"/> object.
        /// </summary>
        /// <param name="state"><see cref="State"/></param>
        /// <returns><see langword="true"/> if successful; <see langword="false"/> otherwise</returns>
        public async Task<bool> UpdateAsync(State state)
        {
            var updateResult = await _context.States.ReplaceOneAsync(
                filter: s => s.FIPS == state.FIPS,
                replacement: state);

            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }

        /// <summary>
        /// Asynchronous hard deletion of a <see cref="State"/> object with <see cref="State.FIPS"/> matching <paramref name="fips"/>.
        /// </summary>
        /// <param name="fips"><see cref="State.FIPS"/></param>
        /// <returns><see langword="true"/> if successful; <see langword="false"/> otherwise</returns>
        public async Task<bool> DeleteAsync(string fips)
        {
            var filter = Builders<State>.Filter.Eq(state => state.FIPS, fips);
            var deleteResult = await _context.States.DeleteOneAsync(filter);

            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }
    }
}
