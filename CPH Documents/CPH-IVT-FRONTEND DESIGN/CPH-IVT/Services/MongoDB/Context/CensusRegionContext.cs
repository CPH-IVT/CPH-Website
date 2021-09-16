using CPH_IVT.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CPH_IVT.Services.MongoDB.Context
{
    /// <summary>
    /// Represents a realization of <see cref="ICensusRegionContext"/>.
    /// </summary>
    public class CensusRegionContext : ICensusRegionContext
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
        public CensusRegionContext(IOptions<HealthDatabaseSettings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _db = client.GetDatabase(options.Value.DatabaseName);
        }

        /// <summary>
        /// MongoDB-specific collection of <see cref="CensusRegion"/> documents.
        /// </summary>
        /// <seealso cref="IMongoCollection{TDocument}"/>
        /// <seealso cref="IMongoDatabase.GetCollection{TDocument}(string, MongoCollectionSettings)"/>
        public IMongoCollection<CensusRegion> CensusRegions => _db.GetCollection<CensusRegion>("Census Regions");
    }
}
