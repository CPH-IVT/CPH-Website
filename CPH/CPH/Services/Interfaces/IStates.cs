using CPH.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPH.Services.Interfaces
{
    public interface IStates
    {
        States Create(States states);
        States Read(int id);
        ICollection<States> ReadAll();
        States Update(States states);
        void Delete(States states);
    }
}
