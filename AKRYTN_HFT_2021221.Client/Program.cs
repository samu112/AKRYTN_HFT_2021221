using AKRYTN_HFT_2021221.Models;
using System;

namespace AKRYTN_HFT_2021221.Client
{
    class Program
    {
        
        static void Main(string[] args)
        {

            Console.WriteLine("Hello World!");

            //Wait for the webserver to start
            System.Threading.Thread.Sleep(8000);

            BookStoreService store = new BookStoreService("http://localhost:8921");

            //Get user with id 1:
            var user1 = store.Get<User>(1, "user");

            Console.WriteLine($"Id: {user1.u_id}\tName: {user1.u_name}");

            //Add Book
            store.Post<Book>(new Book()
            {
                b_title = "Harry Potter",
                b_author = "J.K. Rowling",
                b_price = 5000,
                b_releaseDate = Convert.ToDateTime("2006.02.01 00:00:00")
            }, "book");

            //Get all books
            var books1 = store.Get<Book>("book");

            //Print books1
            foreach (var book in books1)
            {
                Console.WriteLine($"Id:{book.b_id}\tTitle: {book.b_title}");
            }

            //Delete
            store.Delete(2, "book");

            //Get all books
            var books2=store.Get<Book>("book");

            //Print books2
            Console.WriteLine("books2:");
            int bookIDToDelete= 2;
            foreach (var book in books2)
            {
                Console.WriteLine($"Id:{book.b_id}\tTitle: {book.b_title}");
                store.Delete(bookIDToDelete, "book");
                bookIDToDelete++;
            }
            foreach (var book in books2)
            {
                Console.WriteLine($"Id:{book.b_id}\tTitle: {book.b_title}");
            }


            //Wait
            Console.ReadKey();

        }
    }
}
