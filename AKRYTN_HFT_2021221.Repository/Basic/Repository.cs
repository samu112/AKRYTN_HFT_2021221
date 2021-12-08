using AKRYTN_HFT_2021221.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKRYTN_HFT_2021221.Repository
{

    public abstract class Repository<T> : IRepository<T>
        where T : class
    {

        protected BookStoreDbContext dbContext = new BookStoreDbContext();

        public Repository(BookStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public IQueryable<T> GetAll()
        {
            return this.dbContext.Set<T>();
        }


        public abstract T GetOneById(int id);


        public void Insert(T entity)
        {
            this.dbContext.Set<T>().Add(entity);
            this.dbContext.SaveChanges();
        }


        public void Remove(int id)
        {
            this.dbContext.Set<T>().Remove(this.GetOneById(id));
            this.dbContext.SaveChanges();
        }
    }
}
