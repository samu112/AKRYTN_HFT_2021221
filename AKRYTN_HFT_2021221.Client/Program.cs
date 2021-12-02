using AKRYTN_HFT_2021221.Models;
using ConsoleTools;
using System;
using System.Collections.Generic;

namespace AKRYTN_HFT_2021221.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ResetColor();
            Console.WriteLine("Hello World!");
            //Wait for the webserver to start
            //System.Threading.Thread.Sleep(8000);
            menu(args, BookStoreConsoleMenu.store);

        }

        static void menu(string[] args, BookStoreService store)
        {
            var subMenuBook = new ConsoleMenu(args, level: 1)
              .Add("Add book", () => BookStoreConsoleMenu.AddBook())
              .Add("Get book by ID", () => BookStoreConsoleMenu.GetBookById())
              .Add("Get all books", () => BookStoreConsoleMenu.GetAllBooks())
              .Add("Edit book", () => BookStoreConsoleMenu.ChangeBook())
              .Add("Delete book", () => BookStoreConsoleMenu.DeleteBook())
              .Add("Get the publisher of a book", () => BookStoreConsoleMenu.GetBookPublisher())
              .Add("Back", ConsoleMenu.Close)
              .Configure(config =>
              {
                  config.Selector = "--> ";
                  config.EnableFilter = false;
                  config.Title = "Books";
                  config.EnableBreadcrumb = true;
                  config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
              });

            var subMenuCart = new ConsoleMenu(args, level: 1)
              .Add("Add cart", () => BookStoreConsoleMenu.AddCart())
              .Add("Get cart by ID", () => BookStoreConsoleMenu.GetCartById())
              .Add("Get all carts", () => BookStoreConsoleMenu.GetAllCarts())
              .Add("Edit cart", () => BookStoreConsoleMenu.ChangeCart())
              .Add("Delete cart", () => BookStoreConsoleMenu.DeleteCart())
              .Add("Get cart items in this cart", () => BookStoreConsoleMenu.GetCartItemsInThisCart())
              .Add("Get the price of the cart", () => BookStoreConsoleMenu.GetCartPrice())
              .Add("Get the books in this cart", () => BookStoreConsoleMenu.GetBooksInThisCart())
              .Add("Back", ConsoleMenu.Close)
              .Configure(config =>
              {
                  config.Selector = "--> ";
                  config.EnableFilter = false;
                  config.Title = "Carts";
                  config.EnableBreadcrumb = true;
                  config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
              });

            var subMenuUser = new ConsoleMenu(args, level: 1)
              .Add("Add user", () => BookStoreConsoleMenu.AddUser())
              .Add("Get user by ID", () => BookStoreConsoleMenu.GetUserById())
              .Add("Get all users", () => BookStoreConsoleMenu.GetAllUsers())
              .Add("Edit user", () => BookStoreConsoleMenu.ChangeUser())
              .Add("Delete user", () => BookStoreConsoleMenu.DeleteUser())
              .Add("Get user's cart", () => BookStoreConsoleMenu.GetUserCart())
              .Add("Get user's cart items", () => BookStoreConsoleMenu.GetUserCartItems())
              .Add("Back", ConsoleMenu.Close)
              .Configure(config =>
              {
                  config.Selector = "--> ";
                  config.EnableFilter = false;
                  config.Title = "Carts";
                  config.EnableBreadcrumb = true;
                  config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
              });

            var subMenuPublisher = new ConsoleMenu(args, level: 1)
              .Add("Add publisher", () => BookStoreConsoleMenu.AddPublisher())
              .Add("Get publisher by ID", () => BookStoreConsoleMenu.GetPublisherById())
              .Add("Get all publishers", () => BookStoreConsoleMenu.GetAllPublishers())
              .Add("Edit publisher", () => BookStoreConsoleMenu.ChangePublisher())
              .Add("Delete publisher", () => BookStoreConsoleMenu.DeletePublisher())
              .Add("Get publisher's books", () => BookStoreConsoleMenu.GetPublisherBooks())
              .Add("Back", ConsoleMenu.Close)
              .Configure(config =>
              {
                  config.Selector = "--> ";
                  config.EnableFilter = false;
                  config.Title = "Publishers";
                  config.EnableBreadcrumb = true;
                  config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
              });

            var subMenuCartItem = new ConsoleMenu(args, level: 1)
              .Add("Add cart item", () => BookStoreConsoleMenu.AddCartItem())
              .Add("Get cart item by ID", () => BookStoreConsoleMenu.GetCartItemById())
              .Add("Get all cart items", () => BookStoreConsoleMenu.GetAllCartItem())
              .Add("Edit cart item", () => BookStoreConsoleMenu.ChangeCartItem())
              .Add("Delete cart item", () => BookStoreConsoleMenu.DeleteCartItem())
              .Add("Back", ConsoleMenu.Close)
              .Configure(config =>
              {
                  config.Selector = "--> ";
                  config.EnableFilter = false;
                  config.Title = "CartItems";
                  config.EnableBreadcrumb = true;
                  config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
              });

            var menu = new ConsoleMenu(args, level: 0)
                .Add("User", subMenuUser.Show)
                .Add("Cart", subMenuCart.Show)
                .Add("CartItem", subMenuCartItem.Show)
                .Add("Books", subMenuBook.Show)
                .Add("Publisher", subMenuPublisher.Show)
                //.Add("Close", ConsoleMenu.Close)
                .Add("Exit", () => Environment.Exit(0))
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.EnableFilter = false;
                    config.Title = "Main menu";
                    config.EnableWriteTitle = true;
                    config.EnableBreadcrumb = false;
                });

            menu.Show();
        }
    }
}
