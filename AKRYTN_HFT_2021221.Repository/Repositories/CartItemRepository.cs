using AKRYTN_HFT_2021221.Data;
using AKRYTN_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKRYTN_HFT_2021221.Repository
{
    public class CartItemRepository : Repository<CartItem>, ICartItemRepository
    {
        public CartItemRepository(BookStoreDbContext dbContext) : base(dbContext)
        {
        }

        public override CartItem GetOneById(int id)
        {
            return this.GetAll().SingleOrDefault(x => x.ci_id == id);
        }

        public void UpdateBookId(int id, int newBookId)
        {
            var cartItem = this.GetOneById(id);
            cartItem.ci_book_id = newBookId;
            this.dbContext.SaveChanges();
        }

        public void UpdateCartId(int id, int newCartId)
        {
            var cartItem = this.GetOneById(id);
            cartItem.ci_cart_id = newCartId;
            this.dbContext.SaveChanges();
        }

        public void UpdateQuantity(int id, int newQuantity)
        {
            var cartItem = this.GetOneById(id);
            cartItem.ci_quantity = newQuantity;
            this.dbContext.SaveChanges();
        }
    }
}
