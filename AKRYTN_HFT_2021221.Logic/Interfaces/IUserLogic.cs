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
        void AddNewUser(User user);

        User GetUser(int id);

        IEnumerable<User> GetUsers();

        void ChangeUserName(int id, string newName);

        void ChangeUserRegDate(int id, DateTime newRegDate);

        void ChangeUserAddress(int id, string newAddress);

        void ChangeUserEmail(int id, string newEmail);

        void DeleteUser(int id);

        //NON-CRUD
        IEnumerable<string> GetUserCarts(int id);
    }
}
