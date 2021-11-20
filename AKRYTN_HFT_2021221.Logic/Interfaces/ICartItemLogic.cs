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
        void InsertCartItem(CartItem CartItem);

        CartItem GetCartItem(int id);

        IEnumerable<CartItem> GetCartItems();

        void UpdateCartItemBookId(int id, int newBookId);

        void UpdateCartItemCartId(int id, int newCartId);

        void UpdateCartItemQuantity(int id, int newQuanity);

        void DeleteBook(int id);

        //NON-CRUD
        IEnumerable<string> GetCartItems(int id);
    }
}
