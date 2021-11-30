 using AKRYTN_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKRYTN_HFT_2021221.Logic
{
    public interface IPublisherLogic
    {
        //CRUD
        void AddNewPublisher(Publisher publisher);

        Publisher GetPublisher(int id);

        IEnumerable<Publisher> GetPublishers();

        void ChangePublisher(int id, Publisher publisher);

        bool DeletePublisher(int id);

        //NON-CRUD

        //Get the books that were released by the given publisher
        IEnumerable<Book> GetPublisherBooks(int id);
    }
}
