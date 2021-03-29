using CPH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPH.Services.Interfaces
{
    public interface ISavedCharts
    {
        SavedCharts Create(SavedCharts savedCharts);
        SavedCharts Read(int id);
        ICollection<SavedCharts> ReadAll();
        SavedCharts Update(SavedCharts savedCharts);
        void Delete(SavedCharts savedCharts);
    }
}
