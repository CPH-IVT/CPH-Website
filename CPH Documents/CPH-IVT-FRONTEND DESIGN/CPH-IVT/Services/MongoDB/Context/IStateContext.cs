using CPH_IVT.Models;
using MongoDB.Driver;

namespace CPH_IVT.Services.MongoDB.Context
{
    /// <summary>
    /// Provides a mechanism for NoSQL communication with <see cref="State"/> documents.
    /// </summary>
    public interface IStateContext
    {
        /// <summary>
        /// MongoDB-specific collection of <see cref="State"/> documents.
        /// </summary>
        /// <seealso cref="IMongoCollection{TDocument}"/>
        IMongoCollection<State> States { get; }
    }
}