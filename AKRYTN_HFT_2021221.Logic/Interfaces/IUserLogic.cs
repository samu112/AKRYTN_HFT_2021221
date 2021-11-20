using AKRYTN_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKRYTN_HFT_2021221.Logic
{
    public interface IUserLogic
    {
        //CRUD
        void InsertUser(User user);

        User GetUser(int id);

        IEnumerable<User> GetUsers();

        void UpdateUserName(int id, string newName);

        void UpdateUserRegDate(int id, DateTime newRegDate);

        void UpdateUserAddress(int id, string newAddress);

        void UpdateUserEmail(int id, string newEmail);

        void DeleteBook(int id);

        //NON-CRUD
        IEnumerable<string> GetUserCarts(int id);
    }
}
