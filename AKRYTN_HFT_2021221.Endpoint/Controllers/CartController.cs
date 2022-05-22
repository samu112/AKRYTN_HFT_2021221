using AKRYTN_HFT_2021221.Logic;
using AKRYTN_HFT_2021221.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AKRYTN_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CartController
    {
        ICartLogic _cartLogic;
        IHubContext<SignalHub> hub;

        public CartController(ICartLogic cartLogic, IHubContext<SignalHub> hub)
        {
            _cartLogic = cartLogic;
            this.hub = hub;
        }


        //------------------------------------------------------------
        //CRUD:
        //------------------------------------------------------------

        // GET: /Cart
        [HttpGet]
        public IEnumerable<Cart> Get()
        {
            return _cartLogic.GetCarts();
        }

        // GET /Cart/5
        [HttpGet("{id}")]
        public Cart Get(int id)
        {
            return _cartLogic.GetCart(id);
        }

        // POST /Cart
        [HttpPost]
        public void Post([FromBody] Cart value)
        {
            _cartLogic.AddNewCart(value);
            this.hub.Clients.All.SendAsync("CartCreated", value);
        }

        // DELETE /Cart/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Cart cartToDelete = this._cartLogic.GetCart(id);
            _cartLogic.DeleteCart(id);
            this.hub.Clients.All.SendAsync("CartDeleted", cartToDelete);
        }

        //Change Cart quantity
        // PUT /Cart
        [HttpPut]
        public void Put([FromBody] Cart value)
        {
            _cartLogic.ChangeCart(value.c_id, value);
            this.hub.Clients.All.SendAsync("CartUpdated", value);
        }


        //------------------------------------------------------------
        //NON-CRUD:
        //------------------------------------------------------------

        //GET /Cart/1/Carts
        [HttpGet("{id:int}/Cartitems")]
        public IEnumerable<CartItem> GetCartItemsInThisCart(int id)
        {
           return _cartLogic.GetCartItemsInThisCart(id);
        }

        //GET /Cart/1/Price
        [HttpGet("{id:int}/Price")]
        public double GetCartPrice(int id)
        {
            return _cartLogic.GetCartPrice(id);
        }

        //GET /Cart/1/Books
        [HttpGet("{id:int}/Books")]
        public IEnumerable<Book> GetBooksInThisCart(int id)
        {
            return _cartLogic.GetBooksInThisCart(id);
        }
    }
}
