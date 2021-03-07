using CPH_IVT.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CPH_IVT.Services.MongoDB
{
    public class HealthIndicatorContext: IHealthIndicatorContext
    {
        private readonly IMongoDatabase _db;

        public HealthIndicatorContext(IOptions<HealthDatabaseSettings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _db = client.GetDatabase(options.Value.DatabaseName);
        }

        public IMongoCollection<HealthIndicator> HealthIndicators => _db.GetCollection<HealthIndicator>("Health Indicators");

    }
}
