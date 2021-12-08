using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKRYTN_HFT_2021221.Repository
{
    // The skeleton for all descending repositories
    public interface IRepository<T> where T : class
    {
        
        // The generic getall method
        IQueryable<T> GetAll();

        
        // The generic get one method
        T GetOneById(int id);

        // The generic insert method
        void Insert(T entity);

        // The generic remove method
        void Remove(int id);
    }
    
}
