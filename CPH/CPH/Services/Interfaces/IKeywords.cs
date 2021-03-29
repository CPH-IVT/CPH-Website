using CPH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPH.Services.Interfaces
{
    public interface IKeywords
    {
        Keywords Create(Keywords keyword);
        Keywords Read(int id);
        ICollection<Keywords> ReadAll();
        Keywords Update(Keywords keyword);
        void Delete(Keywords keyword);
    }
}
