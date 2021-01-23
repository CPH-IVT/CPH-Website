using CPH_IVT.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CPH_IVT.Services.MongoDB.Repository
{
    /// <summary>
    /// Provides a mechanism for NoSQL CRUD operations on <see cref="CensusDivision"/> documents.
    /// </summary>
    public interface ICensusDivisionRepository
    {

        /// <summary>
        /// Asynchronous creation of a <see cref="CensusDivision"/> object.
        /// </summary>
        /// <param name="censusDivision"><see cref="CensusDivision"/></param>
        Task CreateAsync(CensusDivision censusDivision);

        Task CreateBulkAsync(ICollection<CensusDivision> censusDivisions);

        /// <summary>
        /// Asynchronous retrieval of all <see cref="CensusDivision"/> objects from a MongoDB database.
        /// </summary>
        /// <returns>A collection of <see cref="CensusDivision"/> objects</returns>
        Task<ICollection<CensusDivision>> GetAllAsync();

        /// <summary>
        /// Asynchronous retrieval of a <see cref="CensusDivision"/> object with <see cref="CensusDivision.Number"/> matching <paramref name="divisionNumber"/>.
        /// </summary>
        /// <param name="divisionNumber"><see cref="CensusDivision.Number"/></param>
        /// <returns>A <see cref="CensusDivision"/> object</returns>
        Task<CensusDivision> GetByDivisionNumberAsync(string divisionNumber);

        Task<ICollection<State>> GetAllStatesByDivsionNumberAsync(string divisionNumber);

        Task<ICollection<County>> GetAllCountiesByDivisionNumberAsync(string divisionNumber);

        Task<ICollection<HealthIndicator>> GetAllHealthIndicatorsByDivisionNumberAsync(string divisionNumber);

        /// <summary>
        /// Asynchronous update of a <see cref="CensusDivision"/> object.
        /// </summary>
        /// <param name="censusDivision"><see cref="CensusDivision"/></param>
        /// <returns><see langword="true"/> if successful; <see langword="false"/> otherwise</returns>
        Task<bool> UpdateAsync(CensusDivision censusDivision);

        /// <summary>
        /// Asynchronous hard deletion of a <see cref="CensusDivision"/> object with <see cref="CensusDivision.Number"/> matching <paramref name="divisionNumber"/>.
        /// </summary>
        /// <param name="divisionNumber"><see cref="CensusDivision.Number"/></param>
        /// <returns><see langword="true"/> if successful; <see langword="false"/> otherwise</returns>
        Task<bool> DeleteAsync(string divisionNumber);

        Task<ICollection<CensusDivision>> GetAllDivisionsByRegionNumberAsync(string regionNumber);
    }
}
