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
        private IBookRepository bookRepo;
        private ICartRepository cartRepo;

        //Constructor overload for testing.
        public CartItemLogic(ICartItemRepository cartItemRepo, IBookRepository bookRepo, ICartRepository cartRepo)
        {
            this.cartItemRepo = cartItemRepo;
            this.bookRepo = bookRepo;
            this.cartRepo = cartRepo;
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
            CartItem idlesscartItem = new CartItem();

            //Quantity check
            if (!string.IsNullOrEmpty(cartItem.ci_quantity.ToString()))
            {
                if (cartItem.ci_quantity > 0) { idlesscartItem.ci_quantity = cartItem.ci_quantity; }
                else { throw new ArgumentNullException("CartItem quantity can't be less than 1!"); }
            }
            else { throw new ArgumentNullException("CartItem quantity must have a value!"); }

            //BookId check
            if (!string.IsNullOrEmpty(cartItem.ci_book_id.ToString()))
            {
                var allBooks = bookRepo.GetAll();
                var book = allBooks.Where(book => book.b_id == cartItem.ci_book_id);
                if (book.Count() == 0) 
                {
                    throw new ArgumentException($"There is no book with id: {cartItem.ci_book_id}");
                }
                else { idlesscartItem.ci_book_id = cartItem.ci_book_id; }
            }
            else { throw new ArgumentNullException("CartItem book id must have a value!"); }

            //CartId check
            if (!string.IsNullOrEmpty(cartItem.ci_cart_id.ToString()))
            {
                var allCarts = cartRepo.GetAll();
                var cart = allCarts.Where(cart => cart.c_id == cartItem.ci_cart_id);
                if (cart.Count() == 0)
                {
                    throw new ArgumentException($"There is no cart with id: {cartItem.ci_cart_id}");
                }
                else { idlesscartItem.ci_cart_id = cartItem.ci_cart_id; }
            }
            else { throw new ArgumentNullException("CartItem cart id must have a value!"); }

            //Succesfull addition
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
