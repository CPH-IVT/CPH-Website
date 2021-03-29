using CPH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPH.Services.Interfaces
{
    public interface ICounties
    {
        Counties Create(Counties counties);
        Counties Read(int id);
        ICollection<Counties> ReadAll();
        Counties Update(Counties counties);
        void Delete(Counties counties);
    }
}
