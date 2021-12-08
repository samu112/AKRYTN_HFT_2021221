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

        void ChangeUser(int id, User user);

        bool DeleteUser(int id);

        //NON-CRUD

        //Get the cart of the user
        Cart GetUserCart(int id);

        //Get user's cart items
        IEnumerable<CartItem> GetUserCartItems(int id);

        //UserWithBookOlderThanXyear
        public IEnumerable<User> UserWithBookOlderThanXyear(int year);
    }
}
