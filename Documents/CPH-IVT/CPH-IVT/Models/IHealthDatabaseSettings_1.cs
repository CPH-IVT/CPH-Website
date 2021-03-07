namespace CPH_IVT.Models
{
    public interface IHealthDatabaseSettings
    {
        string CensusDivisionCollectionName { get; set; } //= "Census Divisions";
        string CensusRegionCollectionName { get; set; } //= "Census Regions";
        string CountyCollectionName { get; set; } //= "Counties";
        string CustomRegionCollectionName { get; set; } //= "Custom Regions";
        string HealthIndicatorCollectionName { get; set; } //= "Health Indicators";
        string StateCollectionName { get; set; } //= "States";

        string ConnectionString { get; set; } //= "mongodb+srv://admin:IHateDjangoSoMuch@capstone-ix8cc.mongodb.net/test?retryWrites=true&w=majority";
        string DatabaseName { get; set; } //= "Capstone";
    }
}