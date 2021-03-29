using CPH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPH.Services
{
    public interface ISavedChartKeywords
    {
        SavedChartKeywords Create(SavedChartKeywords savedChartKeywords);
        SavedChartKeywords Read(int id);
        ICollection<SavedChartKeywords> ReadAll();
        SavedChartKeywords Update(SavedChartKeywords savedChartKeywords);
        void Delete(SavedChartKeywords savedChartKeywords);
    }
}
