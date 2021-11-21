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

        public UserLogic()
        {
            this.userRepo = new UserRepository(new Data.BookStoreDbContext());
        }
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
            throw new NotImplementedException();
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

        public void ChangeUserAddress(int id, string newAddress)
        {
            this.userRepo.UpdateAddress(id, newAddress);
        }

        public void ChangeUserEmail(int id, string newEmail)
        {
            this.userRepo.UpdateEmail(id, newEmail);
        }

        public void ChangeUserName(int id, string newName)
        {
            this.userRepo.UpdateName(id, newName);
        }

        public void ChangeUserRegDate(int id, DateTime newRegDate)
        {
            this.userRepo.UpdateRegDate(id, newRegDate);
        }
    }
}
