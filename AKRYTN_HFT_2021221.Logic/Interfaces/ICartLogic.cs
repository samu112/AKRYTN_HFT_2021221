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
        void AddNewCart(Cart cart);

        Cart GetCart(int id);

        IEnumerable<Cart> GetCarts();

        void ChangeCartcreditcardNumber(int id, string newCardNumber);

        void ChangeCartBillingAddress(int id, string newAddress);

        void ChangeCartDeliverStatus(int id, bool newStatus);

        void ChangeCartStatus(int id, bool newStatus);

        void ChangeCartsUser(int id, int newId);

        void DeleteCart(int id);

        //NON-CRUD
        IEnumerable<string> GetItemsInThisCart(int id);
    }
}
