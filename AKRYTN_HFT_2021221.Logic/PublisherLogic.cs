using AKRYTN_HFT_2021221.Models;
using AKRYTN_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKRYTN_HFT_2021221.Logic
{
    public class PublisherLogic : IPublisherLogic
    {
        private readonly IPublisherRepository repo;

        public PublisherLogic()
        {
            this.repo = new PublisherRepository(new Data.BookStoreDbContext());
        }
        //Constructor overload for testing.
        public PublisherLogic(IPublisherRepository repo)
        {
            this.repo = repo;
        }

        //NON-CRUD METHODS:

        public IEnumerable<string> GetPublisherBooks(int id)
        {
            throw new NotImplementedException();
        }

        //CRUD METHODS:

        public void DeletePublisher(int id)
        {
            throw new NotImplementedException();
        }

        public Publisher GetPublisher(int id)
        {
            return this.repo.GetOneById(id);
        }

        public IEnumerable<Publisher> GetPublishers()
        {
            return this.repo.GetAll().ToList();
        }

        public void AddNewPublisher(Publisher publisher)
        {
            this.repo.Insert(publisher);
        }

        public void ChangePublisherAddress(int id, string newAddress)
        {
            this.repo.UpdateAddress(id, newAddress);
        }

        public void ChangePublisherEmail(int id, string newEmail)
        {
            this.repo.UpdateEmail(id, newEmail);
        }

        public void ChangePublisherName(int id, string newName)
        {
            this.repo.UpdateName(id, newName);
        }

        public void ChangePublisherWebsite(int id, string newWebsite)
        {
            this.repo.UpdateWebsite(id, newWebsite);
        }
    }
}
