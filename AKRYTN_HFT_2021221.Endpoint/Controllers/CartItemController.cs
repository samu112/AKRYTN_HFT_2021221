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
    public class CartItemController : ControllerBase
    {
        ICartItemLogic _cartItemLogic;
        IHubContext<SignalHub> hub;

        public CartItemController(ICartItemLogic cartItemLogic, IHubContext<SignalHub> hub)
        {
            _cartItemLogic = cartItemLogic;
            this.hub = hub;
        }

        // GET: /CartItem
        [HttpGet]
        public IEnumerable<CartItem> Get()
        {
            return _cartItemLogic.GetAllCartItems();
        }

        // GET /CartItem/5
        [HttpGet("{id}")]
        public CartItem Get(int id)
        {
            return _cartItemLogic.GetCartItem(id);
        }

        // POST /CartItem
        [HttpPost]
        public void Post([FromBody] CartItem value)
        {
            _cartItemLogic.AddNewCartItem(value);
            this.hub.Clients.All.SendAsync("CartItemCreated", value);
        }

        // DELETE /CartItem/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            CartItem cartItemToDelete = this._cartItemLogic.GetCartItem(id);
            _cartItemLogic.DeleteCartItem(id);
            this.hub.Clients.All.SendAsync("CartItemDeleted", cartItemToDelete);
        }

        //--------------------------------------------------------------------------------------
        //Updates
        //--------------------------------------------------------------------------------------


        //Change cartItem quantity
        // PUT /cartItem
        [HttpPut]
        public void Put([FromBody] CartItem value)
        {
            _cartItemLogic.ChangeCartItem(value.ci_id, value);
            this.hub.Clients.All.SendAsync("CartItemUpdated", value);
        }
    }
}
