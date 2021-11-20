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
        private readonly IUserRepository repo;

        public UserLogic()
        {
            this.repo = new UserRepository(new Data.BookStoreDbContext());
        }
        //Constructor overload for testing.
        public UserLogic(IUserRepository repo)
        {
            this.repo = repo;
        }

        //NON-CRUD METHODS:

        public IEnumerable<string> GetUserCarts(int id)
        {
            throw new NotImplementedException();
        }

        //CRUD METHODS:

        public void DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public User GetUser(int id)
        {
            return this.repo.GetOneById(id);
        }

        public IEnumerable<User> GetUsers()
        {
            return this.repo.GetAll().ToList();
        }

        public void AddNewUser(User user)
        {
            this.repo.Insert(user);
        }

        public void ChangeUserAddress(int id, string newAddress)
        {
            this.repo.UpdateAddress(id, newAddress);
        }

        public void ChangeUserEmail(int id, string newEmail)
        {
            this.repo.UpdateEmail(id, newEmail);
        }

        public void ChangeUserName(int id, string newName)
        {
            this.repo.UpdateName(id, newName);
        }

        public void ChangeUserRegDate(int id, DateTime newRegDate)
        {
            this.repo.UpdateRegDate(id, newRegDate);
        }
    }
}
