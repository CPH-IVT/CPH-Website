using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CPH.Services.InterfaceModels
{
    public interface ICrud<T>
    {
        Task<T> Read(string Id);
        Task<ICollection<T>> ReadAll();
        Task<T> Update(T item);
        Task Delete(T item);
        Task<T> Create(T item);
    }
}
