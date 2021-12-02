using AKRYTN_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AKRYTN_HFT_2021221.Client
{
    public class HelperList : List<string>
    {
        public HelperList(string describerName, List<string> describers, string keyName, List<string> foreignKeys)
        {
            string line = "";
            for (int i = 0; i < foreignKeys.Count; i++)
            {
                line = $"{describerName}: {describers[i]} - {keyName}: {foreignKeys[i]}";
                this.Add(line);
            }
        }
    }

    static class BookStoreConsoleMenu
    {
        private static ConsoleColor defaultColor= Console.ForegroundColor;
        private static ConsoleColor userInputColor = ConsoleColor.Red;
        private static ConsoleColor systemResponseColor = ConsoleColor.Yellow;
        private static ConsoleColor warningMessageColor = ConsoleColor.Blue;
        public static BookStoreService store = new BookStoreService("http://localhost:8921");

        //////////////////
        // Book methods//
        ////////////////

        static public void AddBook()
        {
            //Title
            Write("Add the title of the book: ");
            string title = ReadLine(true);

            //Author
            Write("Add the author of the book: ");
            string author = ReadLine(true);

            //Price
            Write("Add the price of the book: ");
            string price = ReadOnlyDigit(true).ToString();

            //Release Date
            Write("\nAdd the release date of the book: ");
            Color(userInputColor.ToString());
            string releaseDate = ConsoleReadDate.GetDate(true, Convert.ToDateTime("1900.01.01"), DateTime.Now);
            Color(defaultColor.ToString());

            //Publisher ID
            Write("\nAdd publisher Id: ");
            List<Publisher> publishers = store.Get<Publisher>("publisher");
            List<string> publisherName = publishers.Select(publisher => publisher.p_name).ToList();
            List<string> publisId = publishers.Select(publisher => publisher.p_id.ToString()).ToList();
            ForeignKeyHelper(new HelperList("Name", publisherName, "id", publisId));
            int publisherId = ForeignKeyReader(true, publishers.Select(publisher => publisher.p_id).ToList());
            //remove foreignKeyHelper
            (int, int) cursorPosition = (Console.CursorLeft, Console.CursorTop);
            for (int i = 0; i < 5; i++)
            {
                Console.CursorTop += 1;
                ClearCurrentConsoleLine();
            }
            Console.SetCursorPosition(cursorPosition.Item1, cursorPosition.Item2);
            //Finalize
            Color(warningMessageColor);
            Console.WriteLine("\n\nDo want to add the book? (Y/N)");
            ColorReset();
            string answer = ReadLine(false);
            if (answer.ToLower() == "true" || answer.ToLower() == "yes" || answer.ToLower() == "y")
            {
                store.Post<Book>(new Book
                {
                    b_title = title,
                    b_author = author,
                    b_price = Convert.ToDouble(price),
                    b_releaseDate = Convert.ToDateTime(releaseDate),
                    b_publisher_id = publisherId
                }, "book");
                WriteLine("Book succesfully added...");
                Console.ReadKey();
            }
            else
            {
                WriteLine("Book addition cancelled...");
                Console.ReadKey();
            }
            Color(defaultColor);
        }

        static public void GetBookById()
        {
            Write("Add the id of the book: ");
            int id = Convert.ToInt32(ReadLine(true));
            try
            {
                Book book = store.Get<Book>(id, "book");
                WriteLine("\n"+book.ToString());
            }
            catch (NullReferenceException)
            {
                Color(warningMessageColor);
                WriteLine($"Couldn't find a book with id: {id}!");
                ColorReset();
            }
            Console.ReadKey();
        }

        static public void GetAllBooks()
        {
            List<Book> books = store.Get<Book>("book");
            foreach (var book in books)
            {
                WriteLine(book.ToString());
            }
            Console.ReadKey();
        }

        static public void ChangeBook()
        {
            //ID
            WriteLine("(If you don't want to change something just leave it blank and skip with enter)\n");
            Write("Give the ID of the book: ");
            int bookId = ReadOnlyDigit(true);
            Console.WriteLine();

            //Title
            Write("Add the title of the book: ");
            string title = ReadLine(false);

            //Author
            Write("Add the author of the book: ");
            string author = ReadLine(false);

            //Price
            Write("Add the price of the book: ");
            string price = ReadOnlyDigit(false).ToString();

            //Release Date
            Write("\nAdd the release date of the book: ");
            Color(userInputColor.ToString());
            string releaseDate = ConsoleReadDate.GetDate(false, Convert.ToDateTime("1900.01.01"), DateTime.Now);
            Color(defaultColor.ToString());

            //Publisher ID
            Write("\nAdd publisher Id: ");
            List<Publisher> publishers = store.Get<Publisher>("publisher");
            List<string> publisherName = publishers.Select(publisher => publisher.p_name).ToList();
            List<string> publisId = publishers.Select(publisher => publisher.p_id.ToString()).ToList();
            ForeignKeyHelper(new HelperList("Name", publisherName, "id", publisId));
            int publisherId = ForeignKeyReader(false, publishers.Select(publisher => publisher.p_id).ToList());
            //remove foreignKeyHelper
            (int, int) cursorPosition = (Console.CursorLeft, Console.CursorTop);
            for (int i = 0; i < 5; i++)
            {
                Console.CursorTop += 1;
                ClearCurrentConsoleLine();
            }
            Console.SetCursorPosition(cursorPosition.Item1, cursorPosition.Item2);
            //Finalize
            Color(warningMessageColor);
            Console.WriteLine("\n\nDo your really want to change the book? (Y/N)");
            ColorReset();
            string answer = ReadLine(false);
            if (answer.ToLower() == "true" || answer.ToLower() == "yes" || answer.ToLower() == "y")
            {
                Book originalBook;
                try
                {
                    originalBook = store.Get<Book>(bookId, "book");
                    if (originalBook == null)
                    {
                        throw new NullReferenceException();
                    }
                }
                catch (NullReferenceException)
                {
                    Color(warningMessageColor);
                    WriteLine($"Couldn't find a book with id: {bookId}!");
                    Console.ReadKey();
                    ColorReset();
                    return;
                }
                //Check what changed
                if (title == "")
                {
                    title = originalBook.b_title;
                }
                if (author == "")
                {
                    author = originalBook.b_author;
                }
                if (price == "-1")
                {
                    price = originalBook.b_price.ToString();
                }
                if (releaseDate == null)
                {
                    releaseDate = originalBook.b_releaseDate.ToString();
                }
                if (publisherId == -1)
                {
                    publisherId = originalBook.b_publisher_id;
                }
                //Initiate change
                store.Put<Book>(new Book
                {
                    b_id = bookId,
                    b_title = title,
                    b_author = author,
                    b_price = Convert.ToDouble(price),
                    b_releaseDate = Convert.ToDateTime(releaseDate),
                    b_publisher_id = publisherId
                }, "book");
                WriteLine("Book succesfully changed...");
                WriteLine("\nBefore:");
                WriteLine(originalBook.ToString());
                WriteLine("\nAfter:");
                Write(store.Get<Book>(bookId, "book").ToString());
                Console.ReadKey();
            }
            else
            {
                WriteLine("Book edit cancelled...");
                Console.ReadKey();
            }
            Color(defaultColor);
        }

        static public void DeleteBook()
        {
            Write("Add the id of the book: ");
            int id = Convert.ToInt32(ReadLine(true));
            try
            {
                Book book = store.Get<Book>(id, "book");
                WriteLine("\n" + book.ToString());
            }
            catch (NullReferenceException)
            {
                Color(warningMessageColor);
                WriteLine($"Couldn't find a book with id: {id}!");
                ColorReset();
                return;
            }

            //Finalize
            Color(warningMessageColor);
            Console.WriteLine("\n\nDo your really want to delete the book(it is final)? (Y/N)");
            ColorReset();
            string answer = ReadLine(false);
            if (answer.ToLower() == "true" || answer.ToLower() == "yes" || answer.ToLower() == "y")
            {
                store.Delete(id, "book");
                WriteLine("Book succesfully deleted...");
                Console.ReadKey();
            }
            else
            {
                WriteLine("Delet cancelled...");
                Console.ReadKey();
            }
        }

        static public void GetBookPublisher()
        {
            Write("Add the id of the book: ");
            int id = Convert.ToInt32(ReadLine(true));
            try
            {
                Book book = store.Get<Book>(id, "book");
                if (book == null)
                {
                    throw new NullReferenceException();
                }
                Publisher publisher = store.BookGetPublisher(id);
                WriteLine("\nBook:\n  " + book.ToString());
                WriteLine("Publisher:\n  " + publisher.ToString());
            }
            catch (NullReferenceException)
            {
                Color(warningMessageColor);
                WriteLine($"Couldn't find a book with id: {id}!");
                ColorReset();
            }
            Console.ReadKey();
        }


        //////////////////
        // Cart methods//
        ////////////////
        
        static public void AddCart()
        {
            //Credit Card Number
            Write("Add the creditcard number: ");
            string creditcartNumber = ReadLine(true);

            //Billing Address
            Write("Add the billing address: ");
            string billingAddress = ReadLine(true);

            //Deliver Status
            Write("Is it delivered? ");
            bool deliver = BoolReader(true);

            //User ID
            Write("\nAdd publisher Id: ");
            List<User> users = store.Get<User>("user");
            List<string> userName = users.Select(user => user.u_name).ToList();
            List<string> userIds = users.Select(user => user.u_id.ToString()).ToList();
            ForeignKeyHelper(new HelperList("Name", userName, "id", userIds));
            int userId = ForeignKeyReader(true, users.Select(user => user.u_id).ToList());
            //remove foreignKeyHelper
            (int, int) cursorPosition = (Console.CursorLeft, Console.CursorTop);
            for (int i = 0; i < 5; i++)
            {
                Console.CursorTop += 1;
                ClearCurrentConsoleLine();
            }
            Console.SetCursorPosition(cursorPosition.Item1, cursorPosition.Item2);

            List<Cart> carts = store.Get<Cart>("cart");
            try
            {
                if (carts.Any(cart => cart.c_user_id == userId))
                {
                    throw new ArgumentException("User already have a cart!\nOne user can only have one cart!");
                }
            }
            catch (ArgumentException e)
            {
                Color(warningMessageColor);
                Console.WriteLine("\n"+e.Message);
                Console.ReadKey();
                Console.ResetColor();
                return;
            }
            //Finalize
            Color(warningMessageColor);
            Console.WriteLine("\n\nDo want to add the cart? (Y/N)");
            ColorReset();
            string answer = ReadLine(false);
            if (answer.ToLower() == "true" || answer.ToLower() == "yes" || answer.ToLower() == "y")
            {
                store.Post<Cart>(new Cart
                {
                    c_creditcardNumber = creditcartNumber,
                    c_billingAddress = billingAddress,
                    c_deliver = deliver,
                    c_user_id = userId
                }, "cart");
                WriteLine("Cart succesfully added...");
                Console.ReadKey();
            }
            else
            {
                WriteLine("Cart addition cancelled...");
                Console.ReadKey();
            }
            Color(defaultColor);
        }

        static public void GetCartById()
        {
            Write("Add the id of the cart: ");
            int id = Convert.ToInt32(ReadLine(true));
            try
            {
                Cart cart = store.Get<Cart>(id, "cart");
                WriteLine("\n" + cart.ToString());
            }
            catch (NullReferenceException)
            {
                Color(warningMessageColor);
                WriteLine($"Couldn't find a cart with id: {id}!");
                ColorReset();
            }
            Console.ReadKey();
        }

        static public void GetAllCarts()
        {
            List<Cart> carts = store.Get<Cart>("cart");
            foreach (var cart in carts)
            {
                WriteLine(cart.ToString());
            }
            Console.ReadKey();
        }

        static public void ChangeCart()
        {
            //ID
            WriteLine("(If you don't want to change something just leave it blank and skip with enter)\n");
            Write("Give the ID of the cart: ");
            int cartId = ReadOnlyDigit(true);
            Console.WriteLine();

            //Credit Card Number
            Write("Add the creditcard number: ");
            string creditcartNumber = ReadLine(false);

            //Billing Address
            Write("Add the billing address: ");
            string billingAddress = ReadLine(false);

            //Deliver Status
            Write("Is it delivered? ");
            bool deliver = BoolReader(false);

            //User ID
            Write("\nAdd publisher Id: ");
            List<User> users = store.Get<User>("user");
            List<string> userName = users.Select(user => user.u_name).ToList();
            List<string> userIds = users.Select(user => user.u_id.ToString()).ToList();
            ForeignKeyHelper(new HelperList("Name", userName, "id", userIds));
            int userId = ForeignKeyReader(true, users.Select(user => user.u_id).ToList());
            //remove foreignKeyHelper
            (int, int) cursorPosition = (Console.CursorLeft, Console.CursorTop);
            for (int i = 0; i < 6; i++)
            {
                Console.CursorTop += 1;
                ClearCurrentConsoleLine();
            }
            Console.SetCursorPosition(cursorPosition.Item1, cursorPosition.Item2);
            List<Cart> carts = store.Get<Cart>("cart");
            try
            {
                if (carts.Any(cart => cart.c_user_id == userId))
                {
                    throw new ArgumentException("User already have a cart!\nOne user can only have one cart!");
                }
            }
            catch (ArgumentException e)
            {
                Color(warningMessageColor);
                Console.WriteLine("\n" + e.Message);
                Console.ReadKey();
                Console.ResetColor();
                return;
            }
            //Finalize
            Color(warningMessageColor);
            Console.WriteLine("\n\nDo your really want to change the cart? (Y/N)");
            ColorReset();
            string answer = ReadLine(false);
            if (answer.ToLower() == "true" || answer.ToLower() == "yes" || answer.ToLower() == "y")
            {
                Cart originalCart;
                try
                {
                    originalCart = store.Get<Cart>(cartId, "cart");
                    if (originalCart == null)
                    {
                        throw new NullReferenceException();
                    }
                }
                catch (NullReferenceException)
                {
                    Color(warningMessageColor);
                    WriteLine($"Couldn't find a cart with id: {cartId}!");
                    Console.ReadKey();
                    ColorReset();
                    return;
                }
                //Check what changed
                if (creditcartNumber == "")
                {
                    creditcartNumber = originalCart.c_creditcardNumber;
                }
                if (billingAddress == "")
                {
                    billingAddress = originalCart.c_billingAddress;
                }
                if (userId == -1)
                {
                    userId = originalCart.c_user_id;
                }
                //Initiate change
                store.Put<Cart>(new Cart
                {
                    c_id=cartId,
                    c_creditcardNumber=creditcartNumber,
                    c_billingAddress =billingAddress,
                    c_deliver=deliver,
                    c_user_id=userId
                }, "cart");
                WriteLine("Cart succesfully changed...");
                WriteLine("\nBefore:");
                WriteLine(originalCart.ToString());
                WriteLine("\nAfter:");
                Write(store.Get<Cart>(cartId, "cart").ToString());
                Console.ReadKey();
            }
            else
            {
                WriteLine("Cart edit cancelled...");
                Console.ReadKey();
            }
            Color(defaultColor);
        }

        static public void DeleteCart()
        {
            Write("Add the id of the cart: ");
            int id = Convert.ToInt32(ReadLine(true));
            try
            {
                Cart cart = store.Get<Cart>(id, "cart");
                WriteLine("\n" + cart.ToString());
            }
            catch (NullReferenceException)
            {
                Color(warningMessageColor);
                WriteLine($"Couldn't find a cart with id: {id}!");
                ColorReset();
                return;
            }

            //Finalize
            Color(warningMessageColor);
            Console.WriteLine("\n\nDo your really want to delete the cart(it is final)? (Y/N)");
            ColorReset();
            string answer = ReadLine(false);
            if (answer.ToLower() == "true" || answer.ToLower() == "yes" || answer.ToLower() == "y")
            {
                store.Delete(id, "cart");
                WriteLine("Cart succesfully deleted...");
                Console.ReadKey();
            }
            else
            {
                WriteLine("Delet cancelled...");
                Console.ReadKey();
            }
        }

        static public void GetCartItemsInThisCart()
        {
            Write("Add the id of the cart: ");
            int cartId = Convert.ToInt32(ReadLine(true));
            try
            {
                Cart cart = store.Get<Cart>(cartId, "cart");
                if (cart == null)
                {
                    throw new NullReferenceException();
                }
                IEnumerable<CartItem> cartItems = store.GetCartItemsInThisCart(cartId);
                WriteLine("__________________________________");
                foreach (var cartItem in cartItems)
                {
                    Console.WriteLine(cartItem.ToString());
                }
            }
            catch (NullReferenceException)
            {
                Color(warningMessageColor);
                WriteLine($"Couldn't find a cart with id: {cartId}!");
                ColorReset();
            }
            Console.ReadKey();
        }

        static public void GetCartPrice()
        {
            Write("Add the id of the cart: ");
            int cartId = Convert.ToInt32(ReadLine(true));
            try
            {
                Cart cart = store.Get<Cart>(cartId, "cart");
                if (cart == null)
                {
                    throw new NullReferenceException();
                }
                double price = store.GetCartPrice(cartId);
                WriteLine(cart.ToString());
                WriteLine($"\tPrice: {price}");
            }
            catch (NullReferenceException)
            {
                Color(warningMessageColor);
                WriteLine($"Couldn't find a cart with id: {cartId}!");
                ColorReset();
            }
            Console.ReadKey();
        }

        static public void GetBooksInThisCart()
        {
            Write("Add the id of the cart: ");
            int cartId = Convert.ToInt32(ReadLine(true));
            try
            {
                Cart cart = store.Get<Cart>(cartId, "cart");
                if (cart == null)
                {
                    throw new NullReferenceException();
                }
                IEnumerable<Book> books = store.GetBooksInThisCart(cartId);
                WriteLine("__________________________________");
                foreach (var book in books)
                {
                    Console.WriteLine(book.ToString());
                }
            }
            catch (NullReferenceException)
            {
                Color(warningMessageColor);
                WriteLine($"Couldn't find a cart with id: {cartId}!");
                ColorReset();
            }
            Console.ReadKey();

        }


        //////////////////
        // User methods//
        ////////////////

        static public void AddUser()
        {
            //Name
            Write("Add the name of the user: ");
            string name = ReadLine(true);

            //Registration Date
            Write("\nAdd the registration date of the user: ");
            Color(userInputColor.ToString());
            string registrationDate = ConsoleReadDate.GetDate(true, Convert.ToDateTime("1900.01.01"), DateTime.Now);
            Color(defaultColor.ToString());

            //Address
            Write("\nAdd the address of the user: ");
            string address = ReadLine(true);

            //Email
            Write("Add the email of the user: ");
            string email = ReadLine(true);

            //Finalize
            Color(warningMessageColor);
            Console.WriteLine("\n\nDo want to add the user? (Y/N)");
            ColorReset();
            string answer = ReadLine(false);
            if (answer.ToLower() == "true" || answer.ToLower() == "yes" || answer.ToLower() == "y")
            {
                store.Post<User>(new User
                {
                    u_name = name,
                    u_regDate = Convert.ToDateTime(registrationDate),
                    u_address = address,
                    u_email = email,
                }, "user");
                WriteLine("User succesfully added...");
                Console.ReadKey();
            }
            else
            {
                WriteLine("User addition cancelled...");
                Console.ReadKey();
            }
            Color(defaultColor);
        }

        static public void GetUserById()
        {
            Write("Add the id of the user: ");
            int id = Convert.ToInt32(ReadLine(true));
            try
            {
                User user = store.Get<User>(id, "user");
                WriteLine("\n" + user.ToString());
            }
            catch (NullReferenceException)
            {
                Color(warningMessageColor);
                WriteLine($"Couldn't find a user with id: {id}!");
                ColorReset();
            }
            Console.ReadKey();
        }

        static public void GetAllUsers()
        {
            List<User> users = store.Get<User>("user");
            foreach (var user in users)
            {
                WriteLine(user.ToString());
            }
            Console.ReadKey();
        }

        static public void ChangeUser()
        {
            //ID
            WriteLine("(If you don't want to change something just leave it blank and skip with enter)\n");
            Write("Give the ID of the user: ");
            int userId = ReadOnlyDigit(true);
            Console.WriteLine();

            //Name
            Write("Add the name of the user: ");
            string name = ReadLine(false);

            //Registration Date
            Write("\nAdd the registration date of the user: ");
            Color(userInputColor.ToString());
            string registrationDate = ConsoleReadDate.GetDate(false, Convert.ToDateTime("1900.01.01"), DateTime.Now);
            Color(defaultColor.ToString());

            //Address
            Write("\nAdd the address of the user: ");
            string address = ReadLine(false);

            //Email
            Write("Add the email of the user: ");
            string email = ReadLine(false);

            //Finalize
            Color(warningMessageColor);
            Console.WriteLine("\n\nDo your really want to change the user? (Y/N)");
            ColorReset();
            string answer = ReadLine(false);
            if (answer.ToLower() == "true" || answer.ToLower() == "yes" || answer.ToLower() == "y")
            {
                User originalUser;
                try
                {
                    originalUser = store.Get<User>(userId, "user");
                    if (originalUser == null)
                    {
                        throw new NullReferenceException();
                    }
                }
                catch (NullReferenceException)
                {
                    Color(warningMessageColor);
                    WriteLine($"Couldn't find a user with id: {userId}!");
                    Console.ReadKey();
                    ColorReset();
                    return;
                }
                //Check what changed
                if (name == "")
                {
                    name = originalUser.u_name;
                }
                if (registrationDate == null)
                {
                    registrationDate = originalUser.u_regDate.ToString();
                }
                if (address == "")
                {
                    address = originalUser.u_address;
                }
                if (email == "")
                {
                    email = originalUser.u_email;
                }
                //Initiate change
                store.Put<User>(new User
                {
                    u_id=originalUser.u_id,
                    u_name = name,
                    u_regDate = Convert.ToDateTime(registrationDate),
                    u_address = address,
                    u_email = email
                }, "user");
                WriteLine("User succesfully changed...");
                WriteLine("\nBefore:");
                WriteLine(originalUser.ToString());
                WriteLine("\nAfter:");
                Write(store.Get<User>(userId, "user").ToString());
                Console.ReadKey();
            }
            else
            {
                WriteLine("User edit cancelled...");
                Console.ReadKey();
            }
            Color(defaultColor);
        }

        static public void DeleteUser()
        {
            Write("Add the id of the user: ");
            int id = Convert.ToInt32(ReadLine(true));
            try
            {
                User user = store.Get<User>(id, "user");
                WriteLine("\n" + user.ToString());
            }
            catch (NullReferenceException)
            {
                Color(warningMessageColor);
                WriteLine($"Couldn't find a user with id: {id}!");
                ColorReset();
                return;
            }

            //Finalize
            Color(warningMessageColor);
            Console.WriteLine("\n\nDo your really want to delete the user(it is final)? (Y/N)");
            ColorReset();
            string answer = ReadLine(false);
            if (answer.ToLower() == "true" || answer.ToLower() == "yes" || answer.ToLower() == "y")
            {
                store.Delete(id, "user");
                WriteLine("User succesfully deleted...");
                Console.ReadKey();
            }
            else
            {
                WriteLine("Delet cancelled...");
                Console.ReadKey();
            }
        }

        static public void GetUserCart()
        {
            //Cart cart = store.GetUserCart()
            Write("Add the id of the cart: ");
            int id = Convert.ToInt32(ReadLine(true));
            try
            {
                User user = store.Get<User>(id, "user"); 
                Cart cart = store.Get<Cart>("cart").Where(cart => cart.c_user_id == id).FirstOrDefault();
                if (user == null)
                {
                    throw new NullReferenceException($"Couldn't find user with id: {id}!");
                }
                if (cart == null)
                {
                    throw new NullReferenceException($"User(id:{id}) don't have cart :(");
                }
                WriteLine("\nUser:\n  " + user.ToString());
                WriteLine("Cart:\n  " + cart.ToString());
            }
            catch (NullReferenceException e)
            {
                Color(warningMessageColor);
                WriteLine(e.Message);
                ColorReset();
            }
            Console.ReadKey();
        }

        static public void GetUserCartItems()
        {
            Write("Add the id of the user: ");
            int userId = Convert.ToInt32(ReadLine(true));
            try
            {
                User user = store.Get<User>(userId, "user");
                if (user == null)
                {
                    throw new NullReferenceException();
                }
                IEnumerable<CartItem> cartItems = store.GetUserCartItems(userId);
                WriteLine("__________________________________");
                foreach (var cartItem in cartItems)
                {
                    Console.WriteLine(cartItem.ToString());
                }
            }
            catch (NullReferenceException)
            {
                Color(warningMessageColor);
                WriteLine($"Couldn't find a user with id: {userId}!");
                ColorReset();
            }
            Console.ReadKey();
        }


        ///////////////////////
        // Publisher methods//
        /////////////////////

        static public void AddPublisher()
        {
            //Name
            Write("Add the name of the publisher: ");
            string name = ReadLine(true);

            //Address
            Write("\nAdd the address of the publisher: ");
            string address = ReadLine(true);

            //Website
            Write("Add the website of the publisher: ");
            string website = ReadLine(true);

            //Email
            Write("Add the email of the publisher: ");
            string email = ReadLine(true);

            //Finalize
            Color(warningMessageColor);
            Console.WriteLine("\n\nDo want to add the publisher? (Y/N)");
            ColorReset();
            string answer = ReadLine(false);
            if (answer.ToLower() == "true" || answer.ToLower() == "yes" || answer.ToLower() == "y")
            {
                store.Post<Publisher>(new Publisher
                {
                    p_name = name,
                    p_address = address,
                    p_website=website,
                    p_email = email,
                }, "publisher");
                WriteLine("Publisher succesfully added...");
                Console.ReadKey();
            }
            else
            {
                WriteLine("Publisher addition cancelled...");
                Console.ReadKey();
            }
            Color(defaultColor);
        }

        static public void GetPublisherById()
        {
            Write("Add the id of the publisher: ");
            int id = Convert.ToInt32(ReadLine(true));
            try
            {
                Publisher publisher = store.Get<Publisher>(id, "publisher");
                WriteLine("\n" + publisher.ToString());
            }
            catch (NullReferenceException)
            {
                Color(warningMessageColor);
                WriteLine($"Couldn't find a publisher with id: {id}!");
                ColorReset();
            }
            Console.ReadKey();
        }

        static public void GetAllPublishers()
        {
            List<Publisher> publishers = store.Get<Publisher>("publisher");
            foreach (var publisher in publishers)
            {
                WriteLine(publisher.ToString());
            }
            Console.ReadKey();
        }

        static public void ChangePublisher()
        {
            //ID
            WriteLine("(If you don't want to change something just leave it blank and skip with enter)\n");
            Write("Give the ID of the publisher: ");
            int publisherId = ReadOnlyDigit(true);
            Console.WriteLine();

            //Name
            Write("Add the name of the publisher: ");
            string name = ReadLine(false);

            //Address
            Write("\nAdd the address of the publisher: ");
            string address = ReadLine(false);

            //Website
            Write("Add the website of the publisher: ");
            string website = ReadLine(false);

            //Email
            Write("Add the email of the publisher: ");
            string email = ReadLine(false);

            //Finalize
            Color(warningMessageColor);
            Console.WriteLine("\n\nDo your really want to change the publisher? (Y/N)");
            ColorReset();
            string answer = ReadLine(false);
            if (answer.ToLower() == "true" || answer.ToLower() == "yes" || answer.ToLower() == "y")
            {
                Publisher originalPublisher;
                try
                {
                    originalPublisher = store.Get<Publisher>(publisherId, "publisher");
                    if (originalPublisher == null)
                    {
                        throw new NullReferenceException();
                    }
                }
                catch (NullReferenceException)
                {
                    Color(warningMessageColor);
                    WriteLine($"Couldn't find a publisher with id: {publisherId}!");
                    Console.ReadKey();
                    ColorReset();
                    return;
                }
                //Check what changed
                if (name == "")
                {
                    name = originalPublisher.p_name;
                }
                if (address == "")
                {
                    address = originalPublisher.p_address;
                }
                if (website == "")
                {
                    website = originalPublisher.p_website;
                }
                if (email == "")
                {
                    email = originalPublisher.p_email;
                }
                //Initiate change
                store.Put<Publisher>(new Publisher
                {
                    p_id = originalPublisher.p_id,
                    p_name = name,
                    p_address = address,
                    p_website = website,
                    p_email = email
                }, "publisher");
                WriteLine("Publisher succesfully changed...");
                WriteLine("\nBefore:");
                WriteLine(originalPublisher.ToString());
                WriteLine("\nAfter:");
                Write(store.Get<Publisher>(publisherId, "publisher").ToString());
                Console.ReadKey();
            }
        }

        static public void DeletePublisher()
        {
            Write("Add the id of the publisher: ");
            int id = Convert.ToInt32(ReadLine(true));
            try
            {
                Publisher publisher = store.Get<Publisher>(id, "publisher");
                WriteLine("\n" + publisher.ToString());
            }
            catch (NullReferenceException)
            {
                Color(warningMessageColor);
                WriteLine($"Couldn't find a publisher with id: {id}!");
                ColorReset();
                return;
            }

            //Finalize
            Color(warningMessageColor);
            Console.WriteLine("\n\nDo your really want to delete the publisher(it is final)? (Y/N)");
            ColorReset();
            string answer = ReadLine(false);
            if (answer.ToLower() == "true" || answer.ToLower() == "yes" || answer.ToLower() == "y")
            {
                store.Delete(id, "publisher");
                WriteLine("Publisher succesfully deleted...");
                Console.ReadKey();
            }
            else
            {
                WriteLine("Delet cancelled...");
                Console.ReadKey();
            }
        }

        static public void GetPublisherBooks()
        {
            Write("Add the id of the publisher: ");
            int publisherId = Convert.ToInt32(ReadLine(true));
            try
            {
                Publisher publisher = store.Get<Publisher>(publisherId, "publisher");
                if (publisher == null)
                {
                    throw new NullReferenceException();
                }
                IEnumerable<Book> books = store.GetPublisherBooks(publisherId);
                WriteLine("__________________________________");
                foreach (var book in books)
                {
                    Console.WriteLine(book.ToString());
                }
            }
            catch (NullReferenceException)
            {
                Color(warningMessageColor);
                WriteLine($"Couldn't find a publisher with id: {publisherId}!");
                ColorReset();
            }
            Console.ReadKey();
        }


        ///////////////////////
        // Cart-Item methods//
        /////////////////////
        
        static public void AddCartItem()
        {
            //Quantity
            Write("Add the quantity of the item: ");
            string quantity = ReadOnlyDigit(true).ToString();

            //Cart ID
            Write("\nAdd cart Id: ");
            List<Cart> carts = store.Get<Cart>("cart");
            List<string> billingAddresses = carts.Select(cart => cart.c_billingAddress).ToList();
            List<string> cartsIds = carts.Select(cart => cart.c_id.ToString()).ToList();
            ForeignKeyHelper(new HelperList("Billing Address", billingAddresses, "id", cartsIds));
            int cartId = ForeignKeyReader(true, carts.Select(cart => cart.c_id).ToList());
            //remove foreignKeyHelper
            (int, int) cursorPosition = (Console.CursorLeft, Console.CursorTop);
            for (int i = 0; i < 5; i++)
            {
                Console.CursorTop += 1;
                ClearCurrentConsoleLine();
            }
            Console.SetCursorPosition(cursorPosition.Item1, cursorPosition.Item2);

            //Book ID
            Write("\nAdd book Id: ");
            List<Book> books = store.Get<Book>("book");
            List<string> bookTitle = books.Select(book => book.b_title).ToList();
            List<string> booksIds = books.Select(book => book.b_id.ToString()).ToList();
            ForeignKeyHelper(new HelperList("Title", bookTitle, "Id", booksIds));
            int bookId = ForeignKeyReader(true, books.Select(book => book.b_id).ToList());
            //remove foreignKeyHelper
            cursorPosition = (Console.CursorLeft, Console.CursorTop);
            for (int i = 0; i < 5; i++)
            {
                Console.CursorTop += 1;
                ClearCurrentConsoleLine();
            }
            Console.SetCursorPosition(cursorPosition.Item1, cursorPosition.Item2);
            //Finalize
            Color(warningMessageColor);
            Console.WriteLine("\n\nDo want to add the cart-item? (Y/N)");
            ColorReset();
            string answer = ReadLine(false);
            if (answer.ToLower() == "true" || answer.ToLower() == "yes" || answer.ToLower() == "y")
            {
                store.Post<CartItem>(new CartItem
                {
                    ci_book_id=bookId,
                    ci_cart_id=cartId,
                    ci_quantity=Convert.ToInt32(quantity)
                }, "cartitem");
                WriteLine("Cart-item succesfully added...");
                Console.ReadKey();
            }
            else
            {
                WriteLine("Cart addition cancelled...");
                Console.ReadKey();
            }
            Color(defaultColor);
        }

        static public void GetCartItemById()
        {
            Write("Add the id of the cart-item: ");
            int id = Convert.ToInt32(ReadLine(true));
            try
            {
                CartItem cartItem = store.Get<CartItem>(id, "cartitem");
                WriteLine("\n" + cartItem.ToString());
            }
            catch (NullReferenceException)
            {
                Color(warningMessageColor);
                WriteLine($"Couldn't find a cart-item with id: {id}!");
                ColorReset();
            }
            Console.ReadKey();
        }

        static public void GetAllCartItem()
        {
            List<CartItem> cartItems = store.Get<CartItem>("cartitem");
            foreach (var cartItem in cartItems)
            {
                WriteLine(cartItem.ToString());
            }
            Console.ReadKey();
        }

        static public void ChangeCartItem()
        {
            //ID
            WriteLine("(If you don't want to change something just leave it blank and skip with enter)\n");
            Write("Give the ID of the cart-item: ");
            int cartItemId = ReadOnlyDigit(true);
            Console.WriteLine();

            //Quantity
            Write("Add the quantity of the item: ");
            string quantity = ReadOnlyDigit(false).ToString();

            //Cart ID
            Write("\nAdd cart Id: ");
            List<Cart> carts = store.Get<Cart>("cart");
            List<string> billingAddresses = carts.Select(cart => cart.c_billingAddress).ToList();
            List<string> cartsIds = carts.Select(cart => cart.c_id.ToString()).ToList();
            ForeignKeyHelper(new HelperList("Billing Address", billingAddresses, "id", cartsIds));
            int cartId = ForeignKeyReader(false, carts.Select(cart => cart.c_id).ToList());
            //remove foreignKeyHelper
            (int, int) cursorPosition = (Console.CursorLeft, Console.CursorTop);
            for (int i = 0; i < 5; i++)
            {
                Console.CursorTop += 1;
                ClearCurrentConsoleLine();
            }
            Console.SetCursorPosition(cursorPosition.Item1, cursorPosition.Item2);

            //Book ID
            Write("\nAdd book Id: ");
            List<Book> books = store.Get<Book>("book");
            List<string> bookTitle = books.Select(book => book.b_title).ToList();
            List<string> booksIds = books.Select(book => book.b_id.ToString()).ToList();
            ForeignKeyHelper(new HelperList("Title", bookTitle, "Id", booksIds));
            int bookId = ForeignKeyReader(false, books.Select(book => book.b_id).ToList());
            //remove foreignKeyHelper
            cursorPosition = (Console.CursorLeft, Console.CursorTop);
            for (int i = 0; i < 5; i++)
            {
                Console.CursorTop += 1;
                ClearCurrentConsoleLine();
            }
            Console.SetCursorPosition(cursorPosition.Item1, cursorPosition.Item2);
            //Finalize
            Color(warningMessageColor);
            Console.WriteLine("\n\nDo your really want to change the cart-item? (Y/N)");
            ColorReset();
            string answer = ReadLine(false);
            if (answer.ToLower() == "true" || answer.ToLower() == "yes" || answer.ToLower() == "y")
            {
                CartItem originalCartItem;
                try
                {
                    originalCartItem = store.Get<CartItem>(cartItemId, "cartitem");
                    if (originalCartItem == null)
                    {
                        throw new NullReferenceException();
                    }
                }
                catch (NullReferenceException)
                {
                    Color(warningMessageColor);
                    WriteLine($"Couldn't find a publisher with id: {cartItemId}!");
                    Console.ReadKey();
                    ColorReset();
                    return;
                }
                //Check what changed
                if (quantity == "-1")
                {
                    quantity = originalCartItem.ci_quantity.ToString();
                }
                if (bookId == -1)
                {
                    bookId = originalCartItem.ci_book_id;
                }
                if (cartId == -1)
                {
                    cartId = originalCartItem.ci_cart_id;
                }
                //Initiate change
                store.Put<CartItem>(new CartItem
                {
                    ci_id = originalCartItem.ci_id,
                    ci_cart_id = cartId,
                    ci_book_id = bookId,
                    ci_quantity = Convert.ToInt32(quantity)
                }, "cartItem");
                WriteLine("Cart-item succesfully changed...");
                WriteLine("\nBefore:");
                WriteLine(originalCartItem.ToString());
                WriteLine("\nAfter:");
                Write(store.Get<CartItem>(cartItemId, "cartitem").ToString());
                Console.ReadKey();
            }
        }

        static public void DeleteCartItem()
        {
            Write("Add the id of the cart-item: ");
            int id = Convert.ToInt32(ReadLine(true));
            try
            {
                CartItem cartItem = store.Get<CartItem>(id, "cartitem");
                WriteLine("\n" + cartItem.ToString());
            }
            catch (NullReferenceException)
            {
                Color(warningMessageColor);
                WriteLine($"Couldn't find a cart-item with id: {id}!");
                ColorReset();
                return;
            }

            //Finalize
            Color(warningMessageColor);
            Console.WriteLine("\n\nDo your really want to delete the cart-item(it is final)? (Y/N)");
            ColorReset();
            string answer = ReadLine(false);
            if (answer.ToLower() == "true" || answer.ToLower() == "yes" || answer.ToLower() == "y")
            {
                store.Delete(id, "cartitem");
                WriteLine("Cart-item succesfully deleted...");
                Console.ReadKey();
            }
            else
            {
                WriteLine("Delet cancelled...");
                Console.ReadKey();
            }
        }

        ////////////////////
        // Helper methods//
        //////////////////

        public static void ClearCurrentConsoleLine()
        {
            int currentRowCursor = Console.CursorLeft;
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(currentRowCursor, currentLineCursor);
        }

        private static void Write(string text)
        {
            ConsoleColor currentForeground = Console.ForegroundColor;
            Console.ForegroundColor = systemResponseColor;

            Console.Write(text);

            Console.ForegroundColor = currentForeground;
        }

        private static void WriteLine(string text)
        {
            ConsoleColor currentForeground = Console.ForegroundColor;
            Console.ForegroundColor = systemResponseColor;

            Console.WriteLine(text);

            Console.ForegroundColor = currentForeground;
        }

        private static bool BoolReader(bool required)
        {
            Write("Write Bool: ");
            int cursorX = Console.CursorLeft;
            Console.ForegroundColor = userInputColor;
            string response = "";
            do
            {
                (int, int) cursorPosition = (Console.CursorLeft, Console.CursorTop);
                for (int i = 0; i < response.Length; i++)
                {
                    Console.Write(" ");
                }
                Console.CursorLeft = cursorPosition.Item1;
                response = "";
                response = Console.ReadLine();
                //reset cursor
                Console.CursorLeft = cursorPosition.Item1;
                Console.CursorTop = cursorPosition.Item2;
            } while ((response.ToLower() != "false" && response.ToLower() != "no" && response.ToLower() != "n" && response.ToLower() != "true" && response.ToLower() != "yes" && response.ToLower() != "y") && (required == true || response != ""));

            Console.CursorLeft = cursorX-12;
            int cycleNumber = Console.WindowWidth - Console.CursorLeft;
            for (int i = 0; i < cycleNumber; i++)
            {
                Console.Write(" ");
            }
            Console.CursorLeft = cursorX - 12;
            //Console.CursorLeft -= 12;
            if (response.ToLower() == "false" || response.ToLower() == "no" || response.ToLower() == "n")
            {
                Write("You answered: ");
                Color(userInputColor);
                Console.Write("NO");
                Color(systemResponseColor);
                Console.ReadKey();
                return false;
            }
            else if (response.ToLower() == "true" || response.ToLower() == "yes" || response.ToLower() == "y")
            {
                Write("You answered: ");
                Color(userInputColor);
                Console.Write("YES");
                Color(systemResponseColor);
                Console.ReadKey();
                return true;
            }
            return false;
        }

        private static string ReadLine(bool required)
        {
            ConsoleColor currentForeground = Console.ForegroundColor;
            Console.ForegroundColor = userInputColor;
            string response = null;
            do
            {
                (int, int) cursorPosition = (Console.CursorLeft, Console.CursorTop);
                response = Console.ReadLine();
                //reset cursor
                Console.CursorLeft = cursorPosition.Item1;
                Console.CursorTop = cursorPosition.Item2;
            } while ((string.IsNullOrEmpty(response) && string.IsNullOrWhiteSpace(response)) && required);


            //Jump to new line
            Console.CursorTop++;
            Console.CursorLeft = 0;

            Console.ForegroundColor = currentForeground;

            return response;
        }

        private static void Color(string colorname)
        {
            ConsoleColor currentForeground = Console.ForegroundColor;
            ConsoleColor c;
            if (Enum.TryParse(colorname, out c))
            {
                Console.ForegroundColor = c;
            }
            else { Console.ForegroundColor = currentForeground; }

        }

        private static void Color(ConsoleColor colorname)
        {
            Console.ForegroundColor = colorname;
        }

        private static void ColorReset()
        {
            Console.ForegroundColor = defaultColor;
        }

        private static void ForeignKeyHelper(HelperList helperList)
        {
            (int, int) cursorPosition = (Console.CursorLeft, Console.CursorTop);
            int numberOfSuggestions = 3;
            if (helperList.Count < numberOfSuggestions) { numberOfSuggestions = helperList.Count; }
            WriteLine("\n\n------------Suggestions------------");
            for (int i = 0; i < numberOfSuggestions; i++)
            {
                WriteLine(helperList[i]);
            }
            WriteLine("------------Suggestions------------");
            Console.CursorLeft = cursorPosition.Item1;
            Console.CursorTop = cursorPosition.Item2;
        }

        private static int ForeignKeyReader(bool required, List<int> foreignKeys)
        {
            ConsoleColor currentForeground = Console.ForegroundColor;
            Console.ForegroundColor = userInputColor;
            string _val = "";
            ConsoleKeyInfo key;
            bool correctKey = false;
            do
            {
                Console.CursorTop += 9;
                ClearCurrentConsoleLine();
                Console.CursorTop -= 9;
                correctKey = false;
                key = Console.ReadKey(true);
                if (_val.Length < 5 || key.Key == ConsoleKey.Enter)
                {
                    if (key.Key == ConsoleKey.Enter && _val != "" && _val != "-1")
                    {
                        Console.CursorLeft -= _val.Length;
                        for (int i = 0; i < _val.Length; i++)
                        {
                            Console.Write(" ");
                        }
                        Console.CursorLeft -= _val.Length;
                        _val = "";
                        Color(warningMessageColor);
                        (int, int) cursor = Console.GetCursorPosition();
                        Console.CursorTop += 9;
                        Console.Write("Foreign key doesn't exist!");
                        Console.SetCursorPosition(cursor.Item1, cursor.Item2);
                        Color(userInputColor);
                    }
                    if (_val == "-1" && key.Key == ConsoleKey.Enter)
                    {
                        return -1;
                    }
                    if (_val == "-1")
                    {
                        _val = "";
                        Console.CursorLeft -= 2;
                        Console.Write("  ");
                        Console.CursorLeft -= 2;
                    }
                    if (_val == "" && required == false && key.Key == ConsoleKey.Enter)
                    {
                        return -1;
                    }
                    if (key.Key != ConsoleKey.Backspace)
                    {
                        double val = 0;
                        bool _x = double.TryParse(key.KeyChar.ToString(), out val);
                        if (_x)
                        {
                            _val += key.KeyChar;
                            Console.Write(key.KeyChar);
                        }
                        else if (_val == "" && key.KeyChar.ToString() == "-")
                        {
                            _val = "-1";
                            Console.Write("-1");
                        }
                    }
                    if (key.Key == ConsoleKey.Backspace && _val.Length > 0)
                    {
                        _val = _val.Substring(0, (_val.Length - 1));
                        Console.Write("\b \b");
                    }
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && _val.Length > 0)
                    {
                        _val = _val.Substring(0, (_val.Length - 1));
                        Console.Write("\b \b");
                    }
                }

                //Handle empty string
                if (_val != "")
                {
                    if (foreignKeys.Contains(Convert.ToInt32(_val)))
                    {
                        correctKey = true;
                        ConsoleKeyInfo secondkey = Console.ReadKey(true);
                        if (secondkey.Key == ConsoleKey.Enter)
                        {
                            return Convert.ToInt32(_val);
                        }
                        if (secondkey.Key == ConsoleKey.Backspace && _val == "")
                        {
                            Console.CursorLeft++;
                        }
                        else if (secondkey.Key == ConsoleKey.Backspace)
                        {
                             _val = _val.Substring(0, (_val.Length - 1));
                            Console.Write("\b \b");
                        }
                    }
                }

            }
            // Stops Receving Keys Once Enter is Pressed
            while ((key.Key != ConsoleKey.Enter || !correctKey));// || _val == "-1" );
            Console.ForegroundColor = currentForeground;
            return Convert.ToInt32(_val);
        }

        private static int ReadOnlyDigit(bool required)
        {
            //Source: https://stackoverflow.com/questions/13106493/how-do-i-only-allow-number-input-into-my-c-sharp-console-application
            ConsoleColor currentForeground = Console.ForegroundColor;
            Console.ForegroundColor = userInputColor;
            string _val = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);
                if (_val == "" && required == false && key.Key == ConsoleKey.Enter)
                {
                    return -1;
                }
                if (key.Key != ConsoleKey.Backspace)
                {
                    double val = 0;
                    bool _x = double.TryParse(key.KeyChar.ToString(), out val);
                    if (_x)
                    {
                        _val += key.KeyChar;
                        Console.Write(key.KeyChar);
                    }
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && _val.Length > 0)
                    {
                        _val = _val.Substring(0, (_val.Length - 1));
                        Console.Write("\b \b");
                    }
                }
            }
            // Stops Receving Keys Once Enter is Pressed
            while (key.Key != ConsoleKey.Enter || _val == "");
            Console.ForegroundColor = currentForeground;
            return Convert.ToInt32(_val);
        } 

    }
}
