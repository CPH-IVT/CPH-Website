using CPH_IVT.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CPH_IVT.Services.MongoDB
{
    public class CustomRegionContext: ICustomRegionContext
    {
        private readonly IMongoDatabase _db;

        public CustomRegionContext(IOptions<HealthDatabaseSettings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _db = client.GetDatabase(options.Value.DatabaseName);
        }

        public IMongoCollection<CustomRegion> CustomRegions => _db.GetCollection<CustomRegion>("Custom Regions");

    }
}
