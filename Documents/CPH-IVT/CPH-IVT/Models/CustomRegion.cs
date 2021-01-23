using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;

namespace CPH_IVT.Models
{
    /// TODO Complete implementation
    /// 
    /// <summary>
    /// Represents a user-defined geographic region.
    /// </summary>
    public class CustomRegion
    {
        /// <summary>
        /// Database identifier.
        /// </summary>
        [BsonId(IdGenerator = typeof(CombGuidGenerator))]
        public Guid Id { get; set; }

        /// <summary>
        /// Full name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Collection of U.S. states or territories
        /// </summary>
        /// <seealso cref="State"/>
        public ICollection<State> States { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public CustomRegion()
        {
            throw new NotImplementedException();
        }
    }
}
