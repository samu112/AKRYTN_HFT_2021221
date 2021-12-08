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
            menu(args, BookStoreConsoleMenuMethods.store);

        }

        static void menu(string[] args, BookStoreService store)
        {
            var subMenuBook = new ConsoleMenu(args, level: 1)
              .Add("Add book", () => BookStoreConsoleMenuMethods.AddBook())
              .Add("Get book by ID", () => BookStoreConsoleMenuMethods.GetBookById())
              .Add("Get all books", () => BookStoreConsoleMenuMethods.GetAllBooks())
              .Add("Edit book", () => BookStoreConsoleMenuMethods.ChangeBook())
              .Add("Delete book", () => BookStoreConsoleMenuMethods.DeleteBook())
              .Add("Get the publisher of a book", () => BookStoreConsoleMenuMethods.GetBookPublisher())
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
              .Add("Add cart", () => BookStoreConsoleMenuMethods.AddCart())
              .Add("Get cart by ID", () => BookStoreConsoleMenuMethods.GetCartById())
              .Add("Get all carts", () => BookStoreConsoleMenuMethods.GetAllCarts())
              .Add("Edit cart", () => BookStoreConsoleMenuMethods.ChangeCart())
              .Add("Delete cart", () => BookStoreConsoleMenuMethods.DeleteCart())
              .Add("Get cart items in this cart", () => BookStoreConsoleMenuMethods.GetCartItemsInThisCart())
              .Add("Get the price of the cart", () => BookStoreConsoleMenuMethods.GetCartPrice())
              .Add("Get the books in this cart", () => BookStoreConsoleMenuMethods.GetBooksInThisCart())
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
              .Add("Add user", () => BookStoreConsoleMenuMethods.AddUser())
              .Add("Get user by ID", () => BookStoreConsoleMenuMethods.GetUserById())
              .Add("Get all users", () => BookStoreConsoleMenuMethods.GetAllUsers())
              .Add("Edit user", () => BookStoreConsoleMenuMethods.ChangeUser())
              .Add("Delete user", () => BookStoreConsoleMenuMethods.DeleteUser())
              .Add("Get user's cart", () => BookStoreConsoleMenuMethods.GetUserCart())
              .Add("Get user's cart items", () => BookStoreConsoleMenuMethods.GetUserCartItems())
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
              .Add("Add publisher", () => BookStoreConsoleMenuMethods.AddPublisher())
              .Add("Get publisher by ID", () => BookStoreConsoleMenuMethods.GetPublisherById())
              .Add("Get all publishers", () => BookStoreConsoleMenuMethods.GetAllPublishers())
              .Add("Edit publisher", () => BookStoreConsoleMenuMethods.ChangePublisher())
              .Add("Delete publisher", () => BookStoreConsoleMenuMethods.DeletePublisher())
              .Add("Get publisher's books", () => BookStoreConsoleMenuMethods.GetPublisherBooks())
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
              .Add("Add cart item", () => BookStoreConsoleMenuMethods.AddCartItem())
              .Add("Get cart item by ID", () => BookStoreConsoleMenuMethods.GetCartItemById())
              .Add("Get all cart items", () => BookStoreConsoleMenuMethods.GetAllCartItem())
              .Add("Edit cart item", () => BookStoreConsoleMenuMethods.ChangeCartItem())
              .Add("Delete cart item", () => BookStoreConsoleMenuMethods.DeleteCartItem())
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
