using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrinhHuuTruong.eBookStore.Repositories.DataAccess;
using TrinhHuuTruong.eBookStore.Repositories.Entity_Model;
using TrinhHuuTruong.eBookStore.Repositories.Repository.Interface;

namespace TrinhHuuTruong.eBookStore.Repositories.Repository
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(BookStoreDBContext context) : base(context)
        {
        }
    }
}
