using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;

namespace BusinessLayer
{
    public interface IServicio<T> 
    {      
        Task<bool> Add(T item);
        Task<bool> Edit(T item);
        Task<bool> Delete(int id);
        Task<T> GetById(int id);
        Task<List<T>> GetAll();
    }
}
