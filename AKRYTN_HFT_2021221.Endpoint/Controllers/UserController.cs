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

        //--------------------------------------------------------------------------------------
        //Updates
        //--------------------------------------------------------------------------------------


        //Change User quantity
        //
        // PUT /User
        [HttpPut]
        public void Put([FromBody] User value)
        {
            _userLogic.ChangeUser(value.u_id, value);
        }
    }
}
