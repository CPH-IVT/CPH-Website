using CPH_IVT.Models;
using MongoDB.Driver;

namespace CPH_IVT.Services.MongoDB
{
    public interface IHealthIndicatorContext
    {
        IMongoCollection<HealthIndicator> HealthIndicators { get; }
    }
}