using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrinhHuuTruong.eBookStore.Repositories.Entity_Model;

namespace TrinhHuuTruong.eBookStore.Services.Interface
{
    public interface IBookService
    {
        IEnumerable<Book> GetAll();
        Task<Book> Get(int id);
        Task<bool> Add(Book book);
        Task<bool> Update(int id, Book book);
        Task<bool> Delete(int id);
        IEnumerable<Book> Search(string search);
    }
}
