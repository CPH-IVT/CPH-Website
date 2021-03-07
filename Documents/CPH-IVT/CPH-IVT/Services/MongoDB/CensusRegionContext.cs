using CPH_IVT.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CPH_IVT.Services.MongoDB
{
    public class CensusRegionContext: ICensusRegionContext
    {
        private readonly IMongoDatabase _db;

        public CensusRegionContext(IOptions<HealthDatabaseSettings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _db = client.GetDatabase(options.Value.DatabaseName);
        }

        public IMongoCollection<CensusRegion> CensusRegions => _db.GetCollection<CensusRegion>("Census Regions");

    }
}
