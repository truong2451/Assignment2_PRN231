using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using TrinhHuuTruong.eBookStore.Repositories.DataAccess;
using TrinhHuuTruong.eBookStore.Repositories.Repository.Interface;

namespace TrinhHuuTruong.eBookStore.Repositories.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly BookStoreDBContext context;
        private readonly DbSet<T> dbSet;

        public GenericRepository(BookStoreDBContext context)
        {
            if(this.context == null)
            {
                this.context = context;
            }
            this.dbSet = this.context.Set<T>();
        }

        public async Task<bool> Add(T item)
        {
            try
            {
                dbSet.Add(item);
                await context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> DeleteById(object id)
        {
            try
            {
                var item = dbSet.Find(id);
                if(item != null)
                {
                    dbSet.Remove(item);
                    await context.SaveChangesAsync();       
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<T> GetAll()
        {
            try
            {
                IQueryable<T> query = dbSet;
                var result = query.ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<T> GetAllWithCondition(Func<T, bool> where, params Expression<Func<T, bool>>[] includes)
        {
            try
            {
                IQueryable<T> query = dbSet;
                foreach (var include in includes)
                {
                    query.Include(include);
                }
                var result = query.Where(where).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<T> GetById(object id)
        {
            try
            {
                return await dbSet.FindAsync(id);
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> Update(object id, T item)
        {
            try
            {
                var check = dbSet.Find(id);
                if(check != null)
                {
                    context.Entry(check).State = EntityState.Modified;
                    dbSet.Update(item);
                    await context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
