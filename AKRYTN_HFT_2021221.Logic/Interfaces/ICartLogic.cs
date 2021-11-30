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

        void ChangeCart(int id, Cart newCart);

        bool DeleteCart(int id);

        //NON-CRUD

        //Get cartItems that belong to this cart
        IEnumerable<CartItem> GetCartItemsInThisCart(int id);

        //Get the amount of money that is needed to pay for the cart content
        double GetCartPrice(int id);

        //Get books that belong to this cart
        IEnumerable<Book> GetBooksInThisCart(int id);
    }
}
