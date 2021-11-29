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

        //Constructor overload for testing.
        public CartLogic(ICartRepository cartRepo, ICartItemRepository cartItemRepo)
        {
            this.cartRepo = cartRepo;
            this.cartItemRepo = cartItemRepo;
        }

        //NON-CRUD METHODS:

        public IEnumerable<string> GetItemsInThisCart(int id)
        {
            throw new NotImplementedException();
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
