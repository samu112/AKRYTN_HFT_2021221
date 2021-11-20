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

        void ChangePublisherName(int id, string newName);

        void ChangePublisherAddress(int id, string newAddress);

        void ChangePublisherWebsite(int id, string newWebsite);

        void ChangePublisherEmail(int id, string newEmail);

        void DeletePublisher(int id);

        //NON-CRUD
        IEnumerable<string> GetPublisherBooks(int id);
    }
}
