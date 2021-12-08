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

        void ChangeBook(int id, Book book);

        bool DeleteBook(int id);

        //NON-CRUD

        //Get publisher of the book
        Publisher GetBookPublisher(int id);

        IEnumerable<IGrouping<string, Book>> GetBooksGroupedByPublisher();


    }
}
