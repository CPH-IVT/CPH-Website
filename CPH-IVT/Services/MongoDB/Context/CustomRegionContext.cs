using CPH_IVT.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CPH_IVT.Services.MongoDB.Context
{
    public class CustomRegionContext : ICustomRegionContext
    {
        /// <summary>
        /// <see cref="IMongoDatabase"/>
        /// </summary>
        private readonly IMongoDatabase _db;

        /// <summary>
        /// Parameterized constructor.
        /// </summary>
        /// <param name="options"><see cref="IOptions{TOptions}"/></param>
        /// <seealso cref="HealthDatabaseSettings"/>
        public CustomRegionContext(IOptions<HealthDatabaseSettings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _db = client.GetDatabase(options.Value.DatabaseName);
        }

        /// <summary>
        /// MongoDB-specific collection of <see cref="CustomRegion"/> documents.
        /// </summary>
        /// <seealso cref="IMongoCollection{TDocument}"/>
        /// <seealso cref="IMongoDatabase.GetCollection{TDocument}(string, MongoCollectionSettings)"/>
        public IMongoCollection<CustomRegion> CustomRegions => _db.GetCollection<CustomRegion>("Custom Regions");
    }
}
