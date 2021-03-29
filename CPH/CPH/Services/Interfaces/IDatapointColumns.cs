using CPH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPH.Services.Interfaces
{
    public interface IDatapointColumns
    {
        DatapointColumns Create(DatapointColumns datapointColumns);
        DatapointColumns Read(int id);
        ICollection<DatapointColumns> ReadAll();
        DatapointColumns Update(DatapointColumns datapointColumns);
        void Delete(DatapointColumns datapointColumns);
    }
}
