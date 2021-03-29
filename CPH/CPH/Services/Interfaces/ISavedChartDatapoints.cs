using CPH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPH.Services.Interfaces
{
    public interface ISavedChartDatapoints
    {
        SavedChartDatapoints Create(SavedChartDatapoints savedChartDatapoints);
        SavedChartDatapoints Read(int id);
        ICollection<SavedChartDatapoints> ReadAll();
        SavedChartYear Update(SavedChartDatapoints savedChartDatapoints);
        void Delete(SavedChartDatapoints savedChartDatapoints);
    }
}
