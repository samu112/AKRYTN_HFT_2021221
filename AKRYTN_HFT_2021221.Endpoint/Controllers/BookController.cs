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
    public class BookController : ControllerBase
    {
        IBookLogic _BookLogic;

        public BookController(IBookLogic bookLogic)
        {
            _BookLogic = bookLogic;
        }

        // GET: /Book
        [HttpGet]
        public IEnumerable<Book> Get()
        {
            return _BookLogic.GetBooks();
        }

        // GET /Book/5
        [HttpGet("{id}")]
        public Book Get(int id)
        {
            return _BookLogic.GetBook(id);
        }

        // POST /Book
        [HttpPost]
        public void Post([FromBody] Book value)
        {
            _BookLogic.AddBook(value);
        }

        // DELETE /Book/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _BookLogic.DeleteBook(id);
        }

        //--------------------------------------------------------------------------------------
        //Updates
        //--------------------------------------------------------------------------------------


        //Change Book quantity
        // PUT /book
        [HttpPatch]
        public void Put([FromBody] int id, string value)
        {
            _BookLogic.ChangeBookTitle(id, value);
        }
    }
}
