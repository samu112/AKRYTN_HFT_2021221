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
        void InsertBook(Book book);

        Book GetBook(int id);
   
        IEnumerable<Book> GetBooks();

        void UpdateBookTitle(int id, string newTitle);

        void UpdateBookAuthor(int id, string newAuthor);

        void UpdatePrice(int id, int newPrice);

        void UpdateReleaseDate(int id, DateTime newDate);

        void UpdatePublisher(int id, int newid);

        void DeleteBook(int id);

        //NON-CRUD
        IEnumerable<string> GetPublisherName(int id);

    }
}
