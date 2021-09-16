using CPH_IVT.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CPH_IVT.Services.MongoDB
{
    public class CountyContext: ICountyContext
    {
        private readonly IMongoDatabase _db;

        public CountyContext(IOptions<HealthDatabaseSettings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _db = client.GetDatabase(options.Value.DatabaseName);
        }

        public IMongoCollection<County> Counties => _db.GetCollection<County>("Counties");

    }
}
