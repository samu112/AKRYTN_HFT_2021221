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


        //------------------------------------------------------------
        //CRUD:
        //------------------------------------------------------------

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

        //Change Book
        //Project help: https://www.c-sharpcorner.com/article/crud-operation-in-asp-net-core-5-web-api/
        //API HELPS: 1; https://www.syncfusion.com/blogs/post/how-to-build-crud-rest-apis-with-asp-net-core-3-1-and-entity-framework-core-create-jwt-tokens-and-secure-apis.aspx
        //           2; https://www.tutorialsteacher.com/webapi/consume-web-api-get-method-in-aspnet-mvc
        //Patch usage: https://www.infoworld.com/article/3206264/how-to-perform-partial-updates-to-rest-web-api-resources.html
        // PUT /book
        [HttpPut]
        public void Put([FromBody] Book value)
        {
            _BookLogic.ChangeBook(value.b_id, value);
        }


        //------------------------------------------------------------
        //NON-CRUD:
        //------------------------------------------------------------

        //GET /Book/1/publisher
        [HttpGet("{id:int}/publisher")]
        public Publisher GetBookPublisher(int id)
        {
            return _BookLogic.GetBookPublisher(id);
        }

        //GET /Book/bookgroupedbypublisher
        [HttpGet("bookgroupedbypublisher")]
        public IEnumerable<IGrouping<string, Book>> GetBooksGroupedByPublisher()
        {
            return _BookLogic.GetBooksGroupedByPublisher();
        }

    }
}
