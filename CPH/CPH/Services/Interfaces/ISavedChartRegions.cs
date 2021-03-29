using CPH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPH.Services.Interfaces
{
    public interface ISavedChartRegions
    {
        SavedChartRegions Create(SavedChartRegions savedChartRegions);
        SavedChartRegions Read(int id);
        ICollection<SavedChartRegions> ReadAll();
        SavedChartRegions Update(SavedChartRegions savedChartRegions);
        void Delete(SavedChartRegions savedChartRegions);
    }
}
