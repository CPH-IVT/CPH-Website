using CPH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPH.Services.Interfaces
{
    public interface ISavedChartYear
    {
        SavedChartYear Create(SavedChartYear savedChartYear);
        SavedChartYear Read(int id);
        ICollection<SavedChartYear> ReadAll();
        SavedChartYear Update(SavedChartYear savedChartYear);
        void Delete(SavedChartYear savedChartYear);
    }
}
