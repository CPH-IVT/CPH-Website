using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPH.Services.InterfaceModels
{
    public interface IReadOnly<T>
    {
        Task<T> Read(string id);
        Task<ICollection<T>> ReadAll();
    }
}
