using AKRYTN_HFT_2021221.Models;
using AKRYTN_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKRYTN_HFT_2021221.Logic
{
    public class CartItemLogic : ICartItemLogic
    {
        private readonly ICartItemRepository repo;

        public CartItemLogic()
        {
            this.repo = new CartItemRepository(new Data.BookStoreDbContext());
        }
        //Constructor overload for testing.
        public CartItemLogic(ICartItemRepository repo)
        {
            this.repo = repo;
        }

        //NON-CRUD METHODS:



        //CRUD METHODS:

        public void DeleteCartItem(int id)
        {
            throw new NotImplementedException();
        }

        public CartItem GetCartItem(int id)
        {
            return this.repo.GetOneById(id);
        }

        public IEnumerable<CartItem> GetAllCartItems()
        {
            return this.repo.GetAll().ToList();
        }

        public void AddNewCartItem(CartItem cartItem)
        {
            this.repo.Insert(cartItem);
        }

        public void ChangeCartItemBookId(int id, int newBookId)
        {
            this.repo.UpdateBookId(id, newBookId);
        }

        public void ChangeCartItemCartId(int id, int newCartId)
        {
            this.repo.UpdateCartId(id, newCartId);
        }

        public void ChangeCartItemQuantity(int id, int newQuanity)
        {
            this.repo.UpdateQuantity(id, newQuanity);
        }
    }
}
