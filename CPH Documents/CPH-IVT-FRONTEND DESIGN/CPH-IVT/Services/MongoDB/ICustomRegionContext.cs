using CPH_IVT.Models;
using MongoDB.Driver;

namespace CPH_IVT.Services.MongoDB
{
    public interface ICustomRegionContext
    {
        IMongoCollection<CustomRegion> CustomRegions { get; }
    }
}