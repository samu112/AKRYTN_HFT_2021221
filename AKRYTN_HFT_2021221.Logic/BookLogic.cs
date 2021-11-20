using AKRYTN_HFT_2021221.Models;
using AKRYTN_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKRYTN_HFT_2021221.Logic
{
    public class BookLogic : IBookLogic
    {
        private readonly IBookRepository repo;

        public BookLogic()
        {
            this.repo = new BookRepository(new Data.BookStoreDbContext());
        }
        //Constructor overload for testing.
        public BookLogic(IBookRepository repo)
        {
            this.repo = repo;
        }

        //NON-CRUD METHODS:

        public IEnumerable<string> GetPublisherName(int id)
        {
            throw new NotImplementedException();
        }

        //CRUD METHODS:

        public void DeleteBook(int id)
        {
            throw new NotImplementedException();
        }

        public Book GetBook(int id)
        {
            return this.repo.GetOneById(id);
        }

        public IEnumerable<Book> GetBooks()
        {
            return this.repo.GetAll().ToList();
        }

        public void InsertBook(Book book)
        {
            this.repo.Insert(book);
        }

        public void UpdateBookAuthor(int id, string newAuthor)
        {
            this.repo.UpdateAuthor(id, newAuthor);
        }

        public void UpdateBookTitle(int id, string newTitle)
        {
            this.repo.UpdateTitle(id, newTitle);
        }

        public void UpdatePrice(int id, int newPrice)
        {
            this.repo.UpdatePrice(id, newPrice);
        }

        public void UpdatePublisher(int id, int newid)
        {
            this.repo.UpdatePublisherid(id, newid);
        }

        public void UpdateReleaseDate(int id, DateTime newDate)
        {
            this.repo.UpdateReleaseDate(id, newDate);
        }
    }
}
