using AKRYTN_HFT_2021221.Models;
using AKRYTN_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKRYTN_HFT_2021221.Logic
{
    public class CartLogic : ICartLogic
    {
        private ICartRepository cartRepo;
        private ICartItemRepository cartItemRepo;
        private IBookRepository bookRepo;
        private IUserRepository userRepo;

        //Constructor overload for testing.
        public CartLogic(ICartRepository cartRepo, ICartItemRepository cartItemRepo, IBookRepository bookRepo, IUserRepository userRepo)
        {
            this.cartRepo = cartRepo;
            this.cartItemRepo = cartItemRepo;
            this.bookRepo = bookRepo;
            this.userRepo = userRepo;
        }

        //NON-CRUD METHODS:

        //Get cartItems that belong to this cart
        public IEnumerable<CartItem> GetCartItemsInThisCart(int id)
        {
            var cartItems = cartItemRepo.GetAll().Where(cartItem => cartItem.ci_cart_id == id);
            return cartItems.ToList();
        }

        //Get the amount of money that is needed to pay for the cart content
        public double GetCartPrice(int id)
        {
            var cartItems = GetCartItemsInThisCart(id);
            var books = bookRepo.GetAll();

            var price = (from cartitems in cartItems
                             from book in books
                             where cartitems.ci_book_id == book.b_id
                             select cartitems.ci_quantity * book.b_price).Sum();
            return price;
        }

        //Get books that belong to this cart
        public IEnumerable<Book> GetBooksInThisCart(int id)
        {
            var cartItems = GetCartItemsInThisCart(id);

            var books = from cartItem in cartItems
                        select bookRepo.GetOneById(cartItem.ci_book_id);

            return books;
        }




        //CRUD METHODS:

        public bool DeleteCart(int id)
        {
            if (cartRepo.GetAll().Any(cart => cart.c_id == id))
            {
                var cartItemsInCart = cartItemRepo.GetAll().Where(cartItem => cartItem.ci_cart_id == id);
                if (cartItemsInCart.Count() != 0)
                {
                    foreach (CartItem cartItem in cartItemsInCart)
                    {
                        cartItemRepo.Remove(cartItem.ci_id);
                    }
                } //If cart has cartItems, delete them as well
                cartRepo.Remove(id);
                return true;
            } //If cart with this Id does EXIST
            else
            {
                return false;
            } //If cart with this Id does NOT exist
        }

        public Cart GetCart(int id)
        {
            return this.cartRepo.GetOneById(id);
        }

        public IEnumerable<Cart> GetCarts()
        {
            return this.cartRepo.GetAll().ToList();
        }

        public void AddNewCart(Cart cart)
        {
            Cart idlessCart = new Cart();

            //CreditCardNumber check
            if (!string.IsNullOrEmpty(cart.c_creditcardNumber) && !string.IsNullOrWhiteSpace(cart.c_creditcardNumber))
            {
                idlessCart.c_creditcardNumber = cart.c_creditcardNumber;
            }
            else { throw new ArgumentNullException("Cart creditcard number must have a value!"); }
            //BillingAddress check
            if (!string.IsNullOrEmpty(cart.c_billingAddress) && !string.IsNullOrWhiteSpace(cart.c_billingAddress))
            {
                idlessCart.c_billingAddress = cart.c_billingAddress;
            }
            else { throw new ArgumentNullException("Cart billing address must have a value!"); }
            //Deliver check
            try { cart.c_deliver = idlessCart.c_deliver; }
            catch (Exception) { throw new ArgumentNullException("Cart deliver status must have a value!"); }
            //UserId check
            if (!string.IsNullOrEmpty(cart.c_user_id.ToString()))
            {
                var allUserss = userRepo.GetAll();
                var user = allUserss.Where(user => user.u_id == cart.c_user_id);
                if (user.Count() == 0)
                {
                    throw new ArgumentException($"There is no user with id: {cart.c_user_id}");
                }
                else { idlessCart.c_user_id = cart.c_user_id; }

            }
            else { throw new ArgumentNullException("Cart user id must have a value"); }

            //Succesfull addition
            this.cartRepo.Insert(cart);
        }

        public void ChangeCart(int id, Cart newCart)
        {
            string newCreditcardNumber = newCart.c_creditcardNumber;
            string newBillingAdress = newCart.c_billingAddress;
            bool newDeliver = newCart.c_deliver;
            int newUserId = newCart.c_user_id;

            Cart oldCart = this.cartRepo.GetOneById(id);

            if (oldCart.c_creditcardNumber != newCreditcardNumber)
            {
                this.cartRepo.UpdateCreditCard(id, newCreditcardNumber);
            }
            if (oldCart.c_billingAddress != newBillingAdress)
            {
                this.cartRepo.UpdateBillingAddress(id, newBillingAdress);
            }
            if (oldCart.c_deliver != newDeliver)
            {
                this.cartRepo.UpdateDeliver(id, newDeliver);
            }
            if (oldCart.c_user_id != newUserId)
            {
                this.cartRepo.UpdateUserId(id, newUserId);
            }
        }
    }
}
