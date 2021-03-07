using CPH_IVT.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CPH_IVT.Services.MongoDB.Repository
{
    /// <summary>
    /// Provides a mechanism for NoSQL CRUD operations on <see cref="County"/> documents.
    /// </summary>
    public interface ICountyRepository
    {
        /// <summary>
        /// Asynchronous creation of a <see cref="County"/> object.
        /// </summary>
        /// <param name="county"><see cref="County"/></param>
        Task CreateAsync(County county);

        Task CreateBulkAsync(ICollection<County> counties);

        /// <summary>
        /// Asynchronous retrieval of all <see cref="County"/> objects from a MongoDB database.
        /// </summary>
        /// <returns>A collection of <see cref="County"/> objects</returns>
        Task<ICollection<County>> GetAllAsync();

        Task<County> GetByCountyIdAsync(string countyId);

        Task<ICollection<HealthIndicator>> GetAllHealthIndicatorsByCountyIdAsync(string countyId);

        /// <summary>
        /// Asynchronous update of a <see cref="County"/> object.
        /// </summary>
        /// <param name="county"><see cref="County"/></param>
        /// <returns><see langword="true"/> if successful; <see langword="false"/> otherwise</returns>
        Task<bool> UpdateAsync(County county);

        /// <summary>
        /// Asynchronous hard deletion of a <see cref="County"/> object with <see cref="County.CountyId"/> matching <paramref name="countyId"/>.
        /// </summary>
        /// <param name="countyId"><see cref="County.CountyId"/></param>
        /// <returns><see langword="true"/> if successful; <see langword="false"/> otherwise</returns>
        Task<bool> DeleteAsync(string countyId);

        Task<ICollection<County>> GetAllCountiesByStateFIPSAsync(string stateFips);
    }
}
