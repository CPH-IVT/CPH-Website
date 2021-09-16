using CPH_IVT.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CPH_IVT.Services.MongoDB
{
    public class CensusDivisionContext : ICensusDivisionContext
    {
        private readonly IMongoDatabase _db;

        //public DbSet<CensusDivision> CensusDivisions { get; set; }
        //public DbSet<CensusRegion> CensusRegions { get; set; }
        //public DbSet<County> Counties { get; set; }
        //public DbSet<CustomRegion> CustomRegions { get; set; }
        //public DbSet<HealthIndicator> HealthIndicators { get; set; }
        //public DbSet<State> States { get; set; }

        public CensusDivisionContext(IOptions<HealthDatabaseSettings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _db = client.GetDatabase(options.Value.DatabaseName);
        }

        public IMongoCollection<CensusDivision> CensusDivisions => _db.GetCollection<CensusDivision>("Census Divisions");
    }
}
