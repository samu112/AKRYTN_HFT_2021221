using AKRYTN_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace AKRYTN_HFT_2021221.Logic
{
    public interface IBookLogic
    {
        //CRUD
        void AddBook(Book book);

        Book GetBook(int id);
   
        IEnumerable<Book> GetBooks();

        void ChangeBookTitle(int id, string newTitle);

        void ChangeBookAuthor(int id, string newAuthor);

        void ChangeBookPrice(int id, int newPrice);

        void ChangeBookReleaseDate(int id, DateTime newDate);

        void ChangeBookPublisher(int id, int newid);

        void DeleteBook(int id);

        //NON-CRUD
        IEnumerable<string> GetBookPublisherName(int id);

    }
}
