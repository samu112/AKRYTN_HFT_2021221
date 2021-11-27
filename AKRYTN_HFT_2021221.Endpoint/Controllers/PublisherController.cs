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

        //--------------------------------------------------------------------------------------
        //Updates
        //--------------------------------------------------------------------------------------


        //Change Publisher quantity
        // PUT /Publisher
        [HttpPatch]
        public void Put([FromBody] int id, string value)
        {
            _PublisherLogic.ChangePublisherName(id, value);
        }
    }
}
