using AKRYTN_HFT_2021221.Models;
using AKRYTN_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKRYTN_HFT_2021221.Logic
{
    public class UserLogic : IUserLogic
    {
        private IUserRepository userRepo;
        private ICartRepository cartRepo;
        private ICartItemRepository cartItemRepo;
        private IBookRepository bookRepo;

        //Constructor overload for testing.
        public UserLogic(IUserRepository userRepo, ICartRepository cartRepo, ICartItemRepository cartItemRepo, IBookRepository bookRepo)
        {
            this.userRepo = userRepo;
            this.cartRepo = cartRepo;
            this.cartItemRepo = cartItemRepo;
            this.bookRepo = bookRepo;
        }

        //NON-CRUD METHODS:

        //Get the cart of the user
        public Cart GetUserCart(int id)
        {
            var carts = cartRepo.GetAll();
            Cart cart = carts.Where(cart => cart.c_user_id == id).FirstOrDefault();
            return cart;
        }

        //Get user's cart items
        public IEnumerable<CartItem> GetUserCartItems(int id)
        {
            Cart cart = GetUserCart(id);
            var allCartItems = cartItemRepo.GetAll();
            var cartItems = from cartItem in allCartItems
                            where cartItem.ci_cart_id == cart.c_id
                            select cartItem;
            return cartItems;
        }

        //CRUD METHODS:

        public bool DeleteUser(int id)
        {
            if (userRepo.GetAll().Any(user => user.u_id == id))
            {
                var userCart = cartRepo.GetAll().Where(cart => cart.c_user_id == id);
                if (userCart.Count() != 0)
                {
                    var cartCartItems = cartItemRepo.GetAll().Where(cartItem => cartItem.ci_cart_id == userCart.SingleOrDefault().c_id);
                    foreach (CartItem cartItem in cartCartItems)
                    {
                        cartItemRepo.Remove(cartItem.ci_id);
                    }
                    cartRepo.Remove(userCart.SingleOrDefault().c_id);
                }//If user have a cart, remove the items from it and then the cart itself
                userRepo.Remove(id);
                return true;
            } //If user with this Id does EXIST
            else
            {
                return false;
            } //If user with this Id does NOT exist
        }

        public User GetUser(int id)
        {
            return this.userRepo.GetOneById(id);
        }

        public IEnumerable<User> GetUsers()
        {
            return this.userRepo.GetAll().ToList();
        }

        public void AddNewUser(User user)
        {
            User idlessUser = new User();

            //Name check
            if (!string.IsNullOrEmpty(user.u_name) && !string.IsNullOrWhiteSpace(user.u_name))
            {
                idlessUser.u_name = user.u_name;
            }
            else { throw new ArgumentNullException("User name must have a value!"); }
            //RegDate check
            if (user.u_regDate != DateTime.MinValue)
            {
                idlessUser.u_regDate = user.u_regDate;
            }
            else { throw new ArgumentException("Add a valid registration date!"); }
            //Address check
            if (!string.IsNullOrEmpty(user.u_address) && !string.IsNullOrWhiteSpace(user.u_address))
            {
                idlessUser.u_address = user.u_address;
            }
            else { throw new ArgumentNullException("User address must have a value!"); }
            //Email check
            if (!string.IsNullOrEmpty(user.u_email) && !string.IsNullOrWhiteSpace(user.u_email))
            {
                idlessUser.u_email = user.u_email;
            }
            else { throw new ArgumentNullException("User email must have a value!"); }

            //Succesfull addition
            this.userRepo.Insert(user);
        }

        public void ChangeUser(int id, User user)
        {
            string newName = user.u_name;
            DateTime newRegDate = user.u_regDate;
            string newAdress = user.u_address;
            string newEmail = user.u_email;

            User oldUser = this.userRepo.GetOneById(id);

            if (oldUser.u_name != newName)
            {
                this.userRepo.UpdateName(id, newName);
            }
            if (oldUser.u_regDate != newRegDate)
            {
                this.userRepo.UpdateRegDate(id, newRegDate);
            }
            if (oldUser.u_address != newAdress)
            {
                this.userRepo.UpdateAddress(id, newAdress);
            }
            if (oldUser.u_email != newEmail)
            {
                this.userRepo.UpdateEmail(id, newEmail);
            }
        }

    }
}
