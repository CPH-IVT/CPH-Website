using CPH.Models;
using CPH.Services.InterfaceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPH.Services.Interfaces
{
    public interface IRegion : ICrud<Region>
    {
        Task<Region> ReadByName(string regionName);
    }
}
