using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrinhHuuTruong.eBookStore.Repositories.Entity_Model;

namespace TrinhHuuTruong.eBookStore.Services.Interface
{
    public interface IPublisherService
    {
        IEnumerable<Publisher> GetAll();
        Task<Publisher> Get(int id);
        Task<bool> Add(Publisher publisher);
        Task<bool> Update(int id, Publisher publisher);
        Task<bool> Delete(int id);
    }
}
