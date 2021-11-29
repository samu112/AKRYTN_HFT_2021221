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

        //Constructor overload for testing.
        public UserLogic(IUserRepository userRepo, ICartRepository cartRepo, ICartItemRepository cartItemRepo)
        {
            this.userRepo = userRepo;
            this.cartRepo = cartRepo;
            this.cartItemRepo = cartItemRepo;
        }

        //NON-CRUD METHODS:

        public IEnumerable<string> GetUserCarts(int id)
        {
            throw new NotImplementedException();
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
