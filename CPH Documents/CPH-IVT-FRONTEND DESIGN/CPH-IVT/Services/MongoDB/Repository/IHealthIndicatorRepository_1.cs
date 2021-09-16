using CPH_IVT.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CPH_IVT.Services.MongoDB.Repository
{
    public interface IHealthIndicatorRepository
    {
        Task<IEnumerable<HealthIndicator>> GetAllHealthIndicators();
        Task<HealthIndicator> GetHealthIndicator(string name);
        Task CreateHealthIndicator(HealthIndicator healthIndicator);
        Task<bool> DeleteHealthIndicator(string name);
    }
}