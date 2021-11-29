using AKRYTN_HFT_2021221.Logic;
using AKRYTN_HFT_2021221.Models;
using Microsoft.AspNetCore.Mvc;
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

        public CartItemController(ICartItemLogic cartItemLogic)
        {
            _cartItemLogic = cartItemLogic;
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
        }

        // DELETE /CartItem/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _cartItemLogic.DeleteCartItem(id);
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
        }
    }
}
