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
    public class CartController
    {
        ICartLogic _cartLogic;

        public CartController(ICartLogic cartLogic)
        {
            _cartLogic = cartLogic;
        }

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
        }

        // DELETE /Cart/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _cartLogic.DeleteCart(id);
        }

        //--------------------------------------------------------------------------------------
        //Updates
        //--------------------------------------------------------------------------------------


        //Change Cart quantity
        // PUT /Cart
        [HttpPatch]
        public void Put([FromBody] int id, string value)
        {
            _cartLogic.ChangeCartBillingAddress(id, value);
        }
    }
}
