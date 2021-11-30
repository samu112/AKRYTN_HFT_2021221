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
    public class PublisherController : ControllerBase
    {
        IPublisherLogic _PublisherLogic;

        public PublisherController(IPublisherLogic publisherLogic)
        {
            _PublisherLogic = publisherLogic;
        }

        //------------------------------------------------------------
        //NON-CRUD:
        //------------------------------------------------------------

        // GET: /Publisher
        [HttpGet]
        public IEnumerable<Publisher> Get()
        {
            return _PublisherLogic.GetPublishers();
        }

        // GET /Publisher/5
        [HttpGet("{id}")]
        public Publisher Get(int id)
        {
            return _PublisherLogic.GetPublisher(id);
        }

        // POST /Publisher
        [HttpPost]
        public void Post([FromBody] Publisher value)
        {
            _PublisherLogic.AddNewPublisher(value);
        }

        // DELETE /Publisher/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _PublisherLogic.DeletePublisher(id);
        }

        // PUT /Publisher
        [HttpPut]
        public void Put([FromBody] Publisher value)
        {
            _PublisherLogic.ChangePublisher(value.p_id, value);
        }


        //------------------------------------------------------------
        //NON-CRUD:
        //------------------------------------------------------------

        // GET /Publisher/5/books
        [HttpGet("{id:int}/books")]
        public IEnumerable<Book> GetPublisherBooks(int id)
        {
            return _PublisherLogic.GetPublisherBooks(id);
        }
    }
}
