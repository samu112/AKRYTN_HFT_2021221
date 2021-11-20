using AKRYTN_HFT_2021221.Models;
using AKRYTN_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKRYTN_HFT_2021221.Logic
{
    class CartItemLogic : ICartItemLogic
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

        public void DeleteBook(int id)
        {
            throw new NotImplementedException();
        }

        public CartItem GetCartItem(int id)
        {
            return this.repo.GetOneById(id);
        }

        public IEnumerable<CartItem> GetCartItems()
        {
            return this.repo.GetAll().ToList();
        }

        public void InsertCartItem(CartItem cartItem)
        {
            this.repo.Insert(cartItem);
        }

        public void UpdateCartItemBookId(int id, int newBookId)
        {
            this.repo.UpdateBookId(id, newBookId);
        }

        public void UpdateCartItemCartId(int id, int newCartId)
        {
            this.repo.UpdateCartId(id, newCartId);
        }

        public void UpdateCartItemQuantity(int id, int newQuanity)
        {
            this.repo.UpdateQuantity(id, newQuanity);
        }
    }
}
