using AKRYTN_HFT_2021221.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKRYTN_HFT_2021221.Repository
{

    public abstract class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {

        protected BookStoreDbContext dbContext = new BookStoreDbContext();

        public Repository(BookStoreDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public IQueryable<TEntity> GetAll()
        {
            return this.dbContext.Set<TEntity>();
        }


        public abstract TEntity GetOneById(int id);


        public void Insert(TEntity entity)
        {
            this.dbContext.Set<TEntity>().Add(entity);
            this.dbContext.SaveChanges();
        }


        public void Remove(int id)
        {
            this.dbContext.Set<TEntity>().Remove(this.GetOneById(id));
            this.dbContext.SaveChanges();
        }
    }
}
