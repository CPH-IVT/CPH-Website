using CPH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPH.Services.Interfaces
{
    public interface ISavedChartCounties
    {
        SavedChartCounties Create(SavedChartCounties savedChartsCounties);
        SavedChartCounties Read(int id);
        ICollection<SavedChartCounties> ReadAll();
        SavedChartCounties Update(SavedChartCounties savedChartsCounties);
        void Delete(SavedChartCounties savedChartsCounties);
    }
}
