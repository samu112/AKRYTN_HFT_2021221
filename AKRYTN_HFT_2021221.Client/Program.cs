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

            var books = store.Get<Book>("book");
            Console.WriteLine("----------------\nBooks:\n----------------");
            foreach (var book in books)
            {
                Console.WriteLine($"ID: {book.b_id}\tTitle: {book.b_title}\tAuthor: {book.b_author}");
            }
            Console.WriteLine("\nPress any button to continue...");
            Console.ReadKey();


        }
    }
}
