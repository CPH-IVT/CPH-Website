namespace CPH_IVT.Models
{
    /// <summary>
    /// Provides a mechanism for mapping model collections to a MongoDB instance.
    /// Consumed by 'appsettings.json'.
    /// </summary>
    public interface IHealthDatabaseSettings
    {
        /// <summary>
        /// Database document collection name for a <see cref="CensusDivision"/>.
        /// </summary>
        string CensusDivisionCollectionName { get; set; }

        /// <summary>
        /// Database document collection name for a <see cref="CensusRegion"/>.
        /// </summary>
        string CensusRegionCollectionName { get; set; }

        /// <summary>
        /// Database document collection name for a <see cref="County"/>.
        /// </summary>
        string CountyCollectionName { get; set; }

        /// <summary>
        /// Database document collection name for a <see cref="CustomRegion"/>.
        /// </summary>
        string CustomRegionCollectionName { get; set; }

        /// <summary>
        /// Database document collection name for a <see cref="HealthIndicator"/>.
        /// </summary>
        string HealthIndicatorCollectionName { get; set; }

        /// <summary>
        /// Database document collection name for a <see cref="State"/>.
        /// </summary>
        string StateCollectionName { get; set; } 

        /// <summary>
        /// Connection string to a running MongoDB instance. 
        /// </summary>
        string ConnectionString { get; set; } 

        /// <summary>
        /// User-friendly name of a running MongoDB instance.
        /// </summary>
        string DatabaseName { get; set; } 
    }
}