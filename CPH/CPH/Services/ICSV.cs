using CPH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPH.Services
{
    public interface ICSV
    {
        CSV Read(int id);
        CSV Create(CSV csv);
        CSV Delete(CSV csv);
        ICollection<CSV> ReadAll();
        CSV Update(CSV csv);
        
    }
}
