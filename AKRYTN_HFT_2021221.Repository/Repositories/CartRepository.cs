using AKRYTN_HFT_2021221.Data;
using AKRYTN_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKRYTN_HFT_2021221.Repository
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        public CartRepository(BookStoreDbContext dbContext) : base(dbContext)
        {
        }

        public override Cart GetOneById(int id)
        {
            return this.GetAll().SingleOrDefault(x => x.c_id == id);
        }

        public void UpdateBillingAddress(int id, string newBillingAddress)
        {
            var cart = this.GetOneById(id);
            cart.c_billingAddress = newBillingAddress;
            this.dbContext.SaveChanges();
        }

        public void UpdateCreditCard(int id, string newCreditCard)
        {
            var cart = this.GetOneById(id);
            cart.c_creditcardNumber = newCreditCard;
            this.dbContext.SaveChanges();
        }

        public void UpdateDeliver(int id, bool newDeliver)
        {
            var cart = this.GetOneById(id);
            cart.c_deliver = newDeliver;
            this.dbContext.SaveChanges();
        }

        public void UpdateUserId(int id, int newUserId)
        {
            var cart = this.GetOneById(id);
            cart.c_user_id = newUserId;
            this.dbContext.SaveChanges();
        }
    }
}
