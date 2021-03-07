namespace CPH_IVT.Models
{
    public class HealthDatabaseSettings : IHealthDatabaseSettings
    {
        public string CensusDivisionCollectionName { get; set; } //= "Census Divisions";
        public string CensusRegionCollectionName { get; set; } //= "Census Regions";
        public string CountyCollectionName { get; set; } //= "Counties";
        public string CustomRegionCollectionName { get; set; } //= "Custom Regions";
        public string HealthIndicatorCollectionName { get; set; } //= "Health Indicators";
        public string StateCollectionName { get; set; } //= "States";

        public string ConnectionString { get; set; } //= "mongodb+srv://admin:IHateDjangoSoMuch@capstone-ix8cc.mongodb.net/test?retryWrites=true&w=majority";
        public string DatabaseName { get; set; } //= "Capstone";
    }
}
