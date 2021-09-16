using CPH_IVT.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CPH_IVT.Services.MongoDB.Repository
{
    /// <summary>
    /// Provides a mechanism for NoSQL CRUD operations on <see cref="CensusRegion"/> documents.
    /// </summary>
    public interface ICensusRegionRepository
    {
        /// <summary>
        /// Asynchronous creation of a <see cref="CensusRegion"/> object.
        /// </summary>
        /// <param name="censusRegion"><see cref="CensusRegion"/></param>
        Task CreateAsync(CensusRegion censusRegion);

        Task CreateBulkAsync(ICollection<CensusRegion> censusRegions);

        /// <summary>
        /// Asynchronous retrieval of all <see cref="CensusRegion"/> objects from a MongoDB database.
        /// </summary>
        /// <returns>A collection of <see cref="CensusRegion"/> objects</returns>
        Task<ICollection<CensusRegion>> GetAllAsync();

        /// <summary>
        /// Asynchronous retrieval of a <see cref="CensusRegion"/> object with <see cref="CensusRegion.Number"/> matching <paramref name="regionNumber"/>.
        /// </summary>
        /// <param name="regionNumber"><see cref="CensusRegion.Number"/></param>
        /// <returns>A <see cref="CensusRegion"/> object</returns>
        Task<CensusRegion> GetByRegionNumberAsync(string regionNumber);

        Task<ICollection<CensusDivision>> GetAllCensusDivisionsByRegionNumberAsync(string regionNumber);

        Task<ICollection<State>> GetAllStatesByRegionNumberAsync(string regionNumber);

        Task<ICollection<County>> GetAllCountiesByRegionNumberAsync(string regionNumber);

        Task<ICollection<HealthIndicator>> GetAllHealthIndicatorsByRegionNumberAsync(string regionNumber);

        /// <summary>
        /// Asynchronous update of a <see cref="CensusRegion"/> object.
        /// </summary>
        /// <param name="censusRegion"><see cref="CensusRegion"/></param>
        /// <returns><see langword="true"/> if successful; <see langword="false"/> otherwise</returns>
        Task<bool> UpdateAsync(CensusRegion censusRegion);

        /// <summary>
        /// Asynchronous hard deletion of a <see cref="CensusRegion"/> object with <see cref="CensusRegion.Number"/> matching <paramref name="regionNumber"/>.
        /// </summary>
        /// <param name="regionNumber"><see cref="CensusRegion.Number"/></param>
        /// <returns><see langword="true"/> if successful; <see langword="false"/> otherwise</returns>
        Task<bool> DeleteAsync(string regionNumber);
    }
}
