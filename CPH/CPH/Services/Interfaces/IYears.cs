using CPH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPH.Services.Interfaces
{
    public interface IYears
    {
        Years Create(Years years);
        Years Read(int id);
        ICollection<Years> ReadAll();
        Years Update(Years years);
        void Delete(Years years);
    }
}
