using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TrinhHuuTruong.eBookStore.Repositories.Repository.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        IEnumerable<T> GetAllWithCondition(Func<T, bool> where, params Expression<Func<T, bool>>[] includes);
        Task<T> GetById(object id);
        Task<bool> Add(T item);
        Task<bool> Update(object id, T item);
        Task<bool> DeleteById(object id);
    }
}
