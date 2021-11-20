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
        void InsertPublisher(Publisher publisher);

        Publisher GetPublisher(int id);

        IEnumerable<Publisher> GetPublishers();

        void UpdatePublisherName(int id, string newName);

        void UpdatePublisherAddress(int id, string newAddress);

        void UpdatePublisherWebsite(int id, string newWebsite);

        void UpdatePublisherEmail(int id, string newEmail);

        void DeleteBook(int id);

        //NON-CRUD
        IEnumerable<string> GetPublisherBooks(int id);
    }
}
