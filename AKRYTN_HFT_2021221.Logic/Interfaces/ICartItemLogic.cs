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

        void ChangeCartItemBookId(int id, int newBookId);

        void ChangeCartItemCartId(int id, int newCartId);

        void ChangeCartItemQuantity(int id, int newQuanity);

        void DeleteCartItem(int id);

        //NON-CRUD
        
    }
}
