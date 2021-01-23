using CPH_IVT.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CPH_IVT.Services.MongoDB.Repository
{
    /// <summary>
    /// Provides a mechanism for CRUD operations on <see cref="State"/> objects.
    /// </summary>
    public interface IStateRepository
    {
        /// <summary>
        /// Asynchronous creation of a <see cref="State"/> object.
        /// </summary>
        /// <param name="state"><see cref="State"/></param>
        Task CreateAsync(State state);

        Task CreateBulkAsync(ICollection<State> states);

        /// <summary>
        /// Asynchronous retrieval of all <see cref="State"/> objects from a MongoDB database.
        /// </summary>
        /// <returns>A collection of <see cref="State"/> objects</returns>
        Task<ICollection<State>> GetAllAsync();

        /// <summary>
        /// Asynchronous retrieval of a <see cref="State"/> object with <see cref="State.FIPS"/> matching <paramref name="fips"/>.
        /// </summary>
        /// <param name="fips"><see cref="State.FIPS"/></param>
        /// <returns>A <see cref="State"/> object</returns>
        Task<State> GetByFIPSAsync(string fips);

        Task<ICollection<County>> GetAllCountiesByStateFIPSAsync(string fips);

        Task<ICollection<HealthIndicator>> GetAllHealthIndicatorsByStateFIPSAsync(string fips);

        /// <summary>
        /// Asynchronous update of a <see cref="State"/> object.
        /// </summary>
        /// <param name="state"><see cref="State"/></param>
        /// <returns><see langword="true"/> if successful; <see langword="false"/> otherwise</returns>
        Task<bool> UpdateAsync(State state);

        /// <summary>
        /// Asynchronous hard deletion of a <see cref="State"/> object with <see cref="State.FIPS"/> matching <paramref name="fips"/>.
        /// </summary>
        /// <param name="fips"><see cref="State.FIPS"/></param>
        /// <returns><see langword="true"/> if successful; <see langword="false"/> otherwise</returns>
        Task<bool> DeleteAsync(string fips);

        Task<ICollection<State>> GetAllStatesByDivisionNumberAsync(string divisionId);
    }
}
