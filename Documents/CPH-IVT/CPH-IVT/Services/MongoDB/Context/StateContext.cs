using CPH_IVT.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace CPH_IVT.Services.MongoDB.Context
{
    /// <summary>
    /// Represents a realization of <see cref="IStateContext"/>.
    /// </summary>
    public class StateContext : IStateContext
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
        public StateContext(IOptions<HealthDatabaseSettings> options)
        {
            var client = new MongoClient(options.Value.ConnectionString);
            _db = client.GetDatabase(options.Value.DatabaseName);
        }

        /// <summary>
        /// MongoDB-specific collection of <see cref="State"/> documents.
        /// </summary>
        /// <seealso cref="IMongoCollection{TDocument}"/>
        /// <seealso cref="IMongoDatabase.GetCollection{TDocument}(string, MongoCollectionSettings)"/>
        public IMongoCollection<State> States => _db.GetCollection<State>("States");
    }
}
