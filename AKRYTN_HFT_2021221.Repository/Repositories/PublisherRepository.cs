using AKRYTN_HFT_2021221.Data;
using AKRYTN_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKRYTN_HFT_2021221.Repository
{
   public class PublisherRepository : Repository<Publisher>, IPublisherRepository
    {
        public PublisherRepository(BookStoreDbContext dbContext) : base(dbContext)
        {
        }

        public override Publisher GetOneById(int id)
        {
            return this.GetAll().SingleOrDefault(x => x.p_id == id);
        }

        public void UpdateAddress(int id, string newAddress)
        {
            var publisher = this.GetOneById(id);
            publisher.p_address = newAddress;
            this.dbContext.SaveChanges();
        }

        public void UpdateEmail(int id, string newEmail)
        {
            var publisher = this.GetOneById(id);
            publisher.p_email = newEmail;
            this.dbContext.SaveChanges();
        }

        public void UpdateName(int id, string newName)
        {
            var publisher = this.GetOneById(id);
            publisher.p_name = newName;
            this.dbContext.SaveChanges();
        }

        public void UpdateWebsite(int id, string newWebsite)
        {
            var publisher = this.GetOneById(id);
            publisher.p_website = newWebsite;
            this.dbContext.SaveChanges();
        }
    }
}
