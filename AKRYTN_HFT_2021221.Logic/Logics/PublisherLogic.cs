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
        private IPublisherRepository publisherRepo;
        private IBookRepository bookRepo;

        public PublisherLogic()
        {
            this.publisherRepo = new PublisherRepository(new Data.BookStoreDbContext());
        }
        //Constructor overload for testing.
        public PublisherLogic(IPublisherRepository publisherRepo, IBookRepository bookRepo)
        {
            this.publisherRepo = publisherRepo;
            this.bookRepo = bookRepo;
        }

        //NON-CRUD METHODS:

        public IEnumerable<string> GetPublisherBooks(int id)
        {
            throw new NotImplementedException();
        }

        //CRUD METHODS:

        public bool DeletePublisher(int id)
        {
            if (publisherRepo.GetAll().Any(publisher => publisher.p_id == id))
            {
                //Find all book with this publisher
                var GetBooksWithThisPublisher = bookRepo.GetAll().Where(book => book.b_publisher_id == id);
                if (GetBooksWithThisPublisher.Count() != 0)
                {
                    foreach (Book book in GetBooksWithThisPublisher)
                    {
                        bookRepo.UpdatePublisherid(book.b_id, -1);//value under 1 means it has no publisher
                    }
                }
                publisherRepo.Remove(id);
                return true;
            } //If publisher with this Id does EXIST
            else
            {
                return false;
            } //If publisher with this Id does NOT exist
        }

        public Publisher GetPublisher(int id)
        {
            return this.publisherRepo.GetOneById(id);
        }

        public IEnumerable<Publisher> GetPublishers()
        {
            return this.publisherRepo.GetAll().ToList();
        }

        public void AddNewPublisher(Publisher publisher)
        {
            this.publisherRepo.Insert(publisher);
        }

        public void ChangePublisherAddress(int id, string newAddress)
        {
            this.publisherRepo.UpdateAddress(id, newAddress);
        }

        public void ChangePublisherEmail(int id, string newEmail)
        {
            this.publisherRepo.UpdateEmail(id, newEmail);
        }

        public void ChangePublisherName(int id, string newName)
        {
            this.publisherRepo.UpdateName(id, newName);
        }

        public void ChangePublisherWebsite(int id, string newWebsite)
        {
            this.publisherRepo.UpdateWebsite(id, newWebsite);
        }
    }
}
