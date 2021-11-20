using AKRYTN_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKRYTN_HFT_2021221.Logic
{
    public interface ICartLogic
    {

        //CRUD
        void InsertCart(Cart Cart);

        Cart GetCart(int id);

        IEnumerable<Cart> GetCarts();

        void UpdateCartcreditcardNumber(int id, string newCardNumber);

        void UpdateCartBillingAddress(int id, string newAddress);

        void UpdateCartDeliver(int id, bool newStatus);

        void UpdateCartStatus(int id, bool newStatus);

        void UpdateCartsUser(int id, int newId);

        void DeleteBook(int id);

        //NON-CRUD
        IEnumerable<string> GetCartItems(int id);
    }
}
