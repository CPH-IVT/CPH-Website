using CPH_IVT.Models;
using MongoDB.Driver;

namespace CPH_IVT.Services.MongoDB
{
    public interface ICountyContext
    {
        IMongoCollection<County> Counties { get; }
    }
}