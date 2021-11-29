using AKRYTN_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKRYTN_HFT_2021221.Logic
{
    public interface ICartItemLogic
    {
        //CRUD
        void AddNewCartItem(CartItem cartItem);

        CartItem GetCartItem(int id);

        IEnumerable<CartItem> GetAllCartItems();

        void ChangeCartItem(int id, CartItem newCartItem);

        bool DeleteCartItem(int id);

        //NON-CRUD
        
    }
}
