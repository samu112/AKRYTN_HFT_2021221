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
    public class PublisherController : ControllerBase
    {
        IPublisherLogic _PublisherLogic;
        IHubContext<SignalHub> hub;

        public PublisherController(IPublisherLogic publisherLogic, IHubContext<SignalHub> hub)
        {
            _PublisherLogic = publisherLogic;
            this.hub = hub;
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
            this.hub.Clients.All.SendAsync("PublisherCreated", value);
        }

        // DELETE /Publisher/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            Publisher publisherToDelete = this._PublisherLogic.GetPublisher(id);
            _PublisherLogic.DeletePublisher(id);
            this.hub.Clients.All.SendAsync("PublisherDeleted", publisherToDelete);
        }

        // PUT /Publisher
        [HttpPut]
        public void Put([FromBody] Publisher value)
        {
            _PublisherLogic.ChangePublisher(value.p_id, value);
            this.hub.Clients.All.SendAsync("PublisherUpdated", value);
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
