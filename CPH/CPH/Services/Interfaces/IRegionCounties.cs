using CPH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPH.Services.Interfaces
{
    public interface IRegionCounties
    {
        RegionCounties Create(RegionCounties regionCounties);
        RegionCounties Read(int id);
        ICollection<RegionCounties> ReadAll();
        RegionCounties Update(RegionCounties regionCounties);
        void Delete(RegionCounties regionCounties);
    }
}
