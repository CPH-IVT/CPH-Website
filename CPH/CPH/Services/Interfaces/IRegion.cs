using CPH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPH.Services.Interfaces
{
    public interface IRegion
    {
        Region Create(Region region);
        Region Read(int id);
        ICollection<Region> ReadAll();
        Region Update(Region region);
        void Delete(Region region);
    }
}
