using AKRYTN_HFT_2021221.Data;
using AKRYTN_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKRYTN_HFT_2021221.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(BookStoreDbContext dbContext) : base(dbContext)
        {
        }

        public override User GetOneById(int id)
        {
            return this.GetAll().SingleOrDefault(x => x.u_Id == id);
        }

        public void UpdateAddress(int id, string newAddres)
        {
            var user = this.GetOneById(id);
            user.u_Address = newAddres;
            this.dbContext.SaveChanges();
        }

        public void UpdateEmail(int id, string newEmail)
        {
            var user = this.GetOneById(id);
            user.u_Email = newEmail;
            this.dbContext.SaveChanges();
        }

        public void UpdateName(int id, string newName)
        {
            var user = this.GetOneById(id);
            user.u_Name = newName;
            this.dbContext.SaveChanges();
        }

        public void UpdateRegDate(int id, DateTime newDate)
        {
            var user = this.GetOneById(id);
            user.u_RegDate = newDate;
            this.dbContext.SaveChanges();
        }

    }
}
