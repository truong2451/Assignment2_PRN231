using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrinhHuuTruong.eBookStore.Repositories.Entity_Model;

namespace TrinhHuuTruong.eBookStore.Services.Interface
{
    public interface IAuthorService
    {
        IEnumerable<Author> GetAll();
        Task<Author> Get(int id);
        Task<bool> Add(Author author);
        Task<bool> Update(int id, Author author);
        Task<bool> Delete(int id);
    }
}
