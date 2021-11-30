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

        //Constructor overload for testing.
        public PublisherLogic(IPublisherRepository publisherRepo, IBookRepository bookRepo)
        {
            this.publisherRepo = publisherRepo;
            this.bookRepo = bookRepo;
        }

        //NON-CRUD METHODS:

        //Get the books that were released by the given publisher
        public IEnumerable<Book> GetPublisherBooks(int id)
        {
            var books = bookRepo.GetAll();
            var pBooks = from book in books
                         where book.b_publisher_id == id
                         select book;
            return pBooks;
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

        public void ChangePublisher(int id, Publisher publisher)
        {
            string newName = publisher.p_name;
            string newAdress = publisher.p_address;
            string newWebsite = publisher.p_website;
            string newEmail = publisher.p_email;

            Publisher oldPublisher = this.publisherRepo.GetOneById(id);

            if (oldPublisher.p_name != newName)
            {
                this.publisherRepo.UpdateName(id, newName);
            }
            if (oldPublisher.p_address != newAdress)
            {
                this.publisherRepo.UpdateAddress(id, newAdress);
            }
            if (oldPublisher.p_website != newWebsite)
            {
                this.publisherRepo.UpdateWebsite(id, newWebsite);
            }
            if (oldPublisher.p_email != newEmail)
            {
                this.publisherRepo.UpdateEmail(id, newEmail);
            }
        }

    }
}
