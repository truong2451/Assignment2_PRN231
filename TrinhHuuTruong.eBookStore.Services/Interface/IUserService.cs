using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrinhHuuTruong.eBookStore.Repositories.Entity_Model;

namespace TrinhHuuTruong.eBookStore.Services.Interface
{
    public interface IUserService
    {
        Task<User> CheckLogin(string email);
    }
}
