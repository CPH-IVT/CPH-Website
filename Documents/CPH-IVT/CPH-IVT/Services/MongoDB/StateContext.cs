using CPH_IVT.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPH_IVT.Services.MongoDB
{
    public class StateContext: IStateContext
    {
        private readonly IMongoDatabase _db;

        public StateContext(IOptions<HealthDatabaseSettings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _db = client.GetDatabase(options.Value.DatabaseName);
        }

        public IMongoCollection<State> States => _db.GetCollection<State>("States");

    }
}
