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

        public void ChangeCartItem(int id, CartItem newCartItem)
        {
            int ci_book_id = newCartItem.ci_book_id;
            int ci_cart_id = newCartItem.ci_cart_id;
            int ci_quantity = newCartItem.ci_quantity;

            CartItem oldCartItem = this.cartItemRepo.GetOneById(id);

            if (oldCartItem.ci_book_id != ci_book_id)
            {
                this.cartItemRepo.UpdateBookId(id, ci_book_id);
            }
            if (oldCartItem.ci_cart_id != ci_cart_id)
            {
                this.cartItemRepo.UpdateCartId(id, ci_cart_id);
            }
            if (oldCartItem.ci_quantity != ci_quantity)
            {
                this.cartItemRepo.UpdateQuantity(id, ci_quantity);
            }
        }

    }
}
