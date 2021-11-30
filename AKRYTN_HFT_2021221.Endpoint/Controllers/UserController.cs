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
    public class UserController
    {
        IUserLogic _userLogic;

        public UserController(IUserLogic userLogic)
        {
            _userLogic = userLogic;
        }

        //------------------------------------------------------------
        //NON-CRUD:
        //------------------------------------------------------------

        // GET: /User
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return _userLogic.GetUsers();
        }

        // GET /User/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return _userLogic.GetUser(id);
        }

        // POST /User
        [HttpPost]
        public void Post([FromBody] User value)
        {
            _userLogic.AddNewUser(value);
        }

        // DELETE /User/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _userLogic.DeleteUser(id);
        }

        // PUT /User
        [HttpPut]
        public void Put([FromBody] User value)
        {
            _userLogic.ChangeUser(value.u_id, value);
        }


        //------------------------------------------------------------
        //NON-CRUD:
        //------------------------------------------------------------

        //GET /User/1/cart
        [HttpGet("{id:int}/cart")]
        public Cart GetUserCart(int id)
        {
            return _userLogic.GetUserCart(id);
        }

        //GET /User/1/cart/cartItems
        [HttpGet("{id:int}/cart/cartItems")]
        public IEnumerable<CartItem> GetUserCartItems(int id)
        {
            return _userLogic.GetUserCartItems(id);
        }

    }
}
