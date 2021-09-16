using CPH_IVT.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CPH_IVT.Services.MongoDB.Repository
{
    /// TODO: Complete implementation
    /// <summary>
    /// Provides a mechanism for NoSQL CRUD operation on <see cref="CustomRegion"/> documents.
    /// </summary>
    public interface ICustomRegionRepository
    {
        Task<ICollection<CustomRegion>> GetAllAsync();

        Task<CustomRegion> GetByRegionNumberAsync(Guid id);

        Task<CustomRegion> GetByNameAsync(string name);

        Task CreateAsync(CustomRegion customRegion);

        Task<bool> UpdateAsync(CustomRegion customRegion);

        Task<bool> DeleteAsync(Guid id);
    }
}
