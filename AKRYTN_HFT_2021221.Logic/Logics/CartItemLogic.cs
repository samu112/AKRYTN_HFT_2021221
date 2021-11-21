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
        private ICartItemRepository cartItemRepo;

        public CartItemLogic()
        {
            this.cartItemRepo = new CartItemRepository(new Data.BookStoreDbContext());
        }
        //Constructor overload for testing.
        public CartItemLogic(ICartItemRepository cartItemRepo)
        {
            this.cartItemRepo = cartItemRepo;
        }

        //NON-CRUD METHODS:



        //CRUD METHODS:

        public bool DeleteCartItem(int id)
        {
            if (cartItemRepo.GetAll().Any(cartItem => cartItem.ci_id == id))
            {
                cartItemRepo.Remove(id);
                return true;
            } //If cartItem with this Id does EXIST
            else
            {
                return false;
            } //If cartItem with this Id does NOT exist
        }

        public CartItem GetCartItem(int id)
        {
            return this.cartItemRepo.GetOneById(id);
        }

        public IEnumerable<CartItem> GetAllCartItems()
        {
            return this.cartItemRepo.GetAll().ToList();
        }

        public void AddNewCartItem(CartItem cartItem)
        {
            this.cartItemRepo.Insert(cartItem);
        }

        public void ChangeCartItemBookId(int id, int newBookId)
        {
            this.cartItemRepo.UpdateBookId(id, newBookId);
        }

        public void ChangeCartItemCartId(int id, int newCartId)
        {
            this.cartItemRepo.UpdateCartId(id, newCartId);
        }

        public void ChangeCartItemQuantity(int id, int newQuanity)
        {
            this.cartItemRepo.UpdateQuantity(id, newQuanity);
        }
    }
}
