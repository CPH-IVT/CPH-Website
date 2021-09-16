namespace CPH_IVT.Models
{
    /// <summary>
    /// Represents a realization of <see cref="IHealthDatabaseSettings"/>.
    /// Consumed by 'appsettings.json'.
    /// </summary>
    public class HealthDatabaseSettings : IHealthDatabaseSettings
    {
        /// <summary>
        /// Database document collection name for a <see cref="CensusDivision"/>.
        /// </summary>
        public string CensusDivisionCollectionName { get; set; }

        /// <summary>
        /// Database document collection name for a <see cref="CensusRegion"/>.
        /// </summary>
        public string CensusRegionCollectionName { get; set; }

        /// <summary>
        /// Database document collection name for a <see cref="County"/>.
        /// </summary>
        public string CountyCollectionName { get; set; }

        /// <summary>
        /// Database document collection name for a <see cref="CustomRegion"/>.
        /// </summary>
        public string CustomRegionCollectionName { get; set; }

        /// <summary>
        /// Database document collection name for a <see cref="HealthIndicator"/>.
        /// </summary>
        public string HealthIndicatorCollectionName { get; set; }

        /// <summary>
        /// Database document collection name for a <see cref="State"/>.
        /// </summary>
        public string StateCollectionName { get; set; }

        /// <summary>
        /// Connection string to a running MongoDB instance. 
        /// </summary>
        public string ConnectionString { get; set; }

        /// <summary>
        /// User-friendly name of a running MongoDB instance.
        /// </summary>
        public string DatabaseName { get; set; }
    }
}