using System;
using Moq;
using AKRYTN_HFT_2021221.Logic;
using AKRYTN_HFT_2021221.Models;
using AKRYTN_HFT_2021221.Repository;
using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;

namespace AKRYTN_HFT_2021221_Test
{

    [TestFixture]
    public class LogicTests
    {
        private Mock<IBookRepository> mockedBookRepo = new Mock<IBookRepository>();
        private Mock<ICartItemRepository> mockedCartItemRepo = new Mock<ICartItemRepository>();
        private Mock<ICartRepository> mockedCartRepo = new Mock<ICartRepository>();
        private Mock<IPublisherRepository> mockedPublisherRepo = new Mock<IPublisherRepository>();

        private List<Publisher> publishersDb = new List<Publisher>() {
            new Publisher() { p_id = 1, p_name = "Better with Books Publisher", p_address = "966 East White Drive, Thomasville NC 27360",    p_email = "Better.with.Books.Publisher@BetterwithBooksPublisher.com", p_website = "BetterwithBooksPublisher.com" },
            new Publisher() { p_id = 2, p_name = "Storylux Publisher",          p_address = "Seattle WA 98144, 722 Stillwater Street",       p_email = "Storylux.Publisher@StoryluxPublisher.com",                 p_website = "StoryluxPublisher.com"        },
            new Publisher() { p_id = 3, p_name = "Literaryify Publisher",       p_address = "8595 Purple Finch Street, Whitestone NY 11357", p_email = "Literaryify.Publisher@LiteraryifyPublisher.com",           p_website = "LiteraryifyPublisher.com"     }
        };
        private List<Book> booksDb = new List<Book>() {
            new Book() { b_id = 1, b_title = "Snakes of the west",        b_author = "Ashley Grimm",       b_price = 3800, b_releaseDate = DateTime.Parse("2019. 06. 25. 0:00:00"), b_publisher_id = 1 },
            new Book() { b_id = 2, b_title = "Broken magic",              b_author = "Haven Poison",       b_price = 7700, b_releaseDate = DateTime.Parse("2019. 09. 30. 0:00:00"), b_publisher_id = 2 },
            new Book() { b_id = 3, b_title = "Enemies of my imagination", b_author = "Glenn Luck",         b_price = 3500, b_releaseDate = DateTime.Parse("2001. 07. 10. 0:00:00"), b_publisher_id = 3 },
            new Book() { b_id = 4, b_title = "Stories in my house",       b_author = "Avalon Viper",       b_price = 8400, b_releaseDate = DateTime.Parse("1989. 12. 22. 0:00:00"), b_publisher_id = 1 },
            new Book() { b_id = 5, b_title = "Descendants of the prison", b_author = "Quinn Griffin",      b_price = 3800, b_releaseDate = DateTime.Parse("1996. 05. 09. 0:00:00"), b_publisher_id = 1 },
            new Book() { b_id = 6, b_title = "Mice without borders",      b_author = "Drake Auteur",       b_price = 3900, b_releaseDate = DateTime.Parse("1989. 06. 30. 0:00:00"), b_publisher_id = 1 },
            new Book() { b_id = 7, b_title = "Begging in the darkness",   b_author = "Sable Braindead",    b_price = 8200, b_releaseDate = DateTime.Parse("2012. 05. 03. 0:00:00"), b_publisher_id = 2 },
            new Book() { b_id = 8, b_title = "Cat with hoods",            b_author = "Rory Hunger",        b_price = 5500, b_releaseDate = DateTime.Parse("2002. 08. 26. 0:00:00"), b_publisher_id = 3 },
            new Book() { b_id = 9, b_title = "Sinners of time",           b_author = "Waverley Ectoplasm", b_price = 2100, b_releaseDate = DateTime.Parse("1978. 02. 14. 0:00:00"), b_publisher_id = 3 }
        };
        private List<CartItem> cartItemsDb = new List<CartItem>() {
            new CartItem() { ci_id = 1, ci_book_id = 2, ci_quantity = 1, ci_cart_id = 2 },
            new CartItem() { ci_id = 2, ci_book_id = 1, ci_quantity = 1, ci_cart_id = 2 },
            new CartItem() { ci_id = 3, ci_book_id = 3, ci_quantity = 4, ci_cart_id = 3 },
            new CartItem() { ci_id = 4, ci_book_id = 1, ci_quantity = 1, ci_cart_id = 3 },
            new CartItem() { ci_id = 5, ci_book_id = 1, ci_quantity = 4, ci_cart_id = 1 },
            new CartItem() { ci_id = 6, ci_book_id = 2, ci_quantity = 1, ci_cart_id = 2 },
            new CartItem() { ci_id = 7, ci_book_id = 3, ci_quantity = 1, ci_cart_id = 3 }
    };
        private List<Cart> cartsDb = new List<Cart>() {
            new Cart() { c_id = 1, c_billingAddress = "200 Sunrise Mall, Massapequa NY 11758",   c_creditcardNumber = "2720059742859433", c_deliver = false, c_user_id = 1 },
            new Cart() { c_id = 2, c_billingAddress = "2001 Glenn Bldv Sw, Fort Payne AL 35968", c_creditcardNumber = "340693154576244",  c_deliver = true,  c_user_id = 2 },
            new Cart() { c_id = 3, c_billingAddress = "625 School Street, Putnam CT 6260",       c_creditcardNumber = "5115698689589010", c_deliver = true,  c_user_id = 3 },
            new Cart() { c_id = 4, c_billingAddress = "295 Plymouth Street, Halifax MA 2338",    c_creditcardNumber = "5596417739847650", c_deliver = false, c_user_id = 4 },
            new Cart() { c_id = 5, c_billingAddress = "279 Troy Road, East Greenbush NY 12061",  c_creditcardNumber = "4916047365012588", c_deliver = true,  c_user_id = 5 }
        };
        private List<User> usersDb = new List<User>() {
            new User() { u_id = 1, u_name = "Bernard Dyer",     u_regDate = DateTime.Parse("2015. 11. 05. 2:20:43"),  u_address = "200 Sunrise Mall, Massapequa NY 11758",          u_email = "Bernard.Dyer@mail.com"           },
            new User() { u_id = 2, u_name = "Kasen Williams",   u_regDate = DateTime.Parse("2012. 04. 29. 21:40:21"), u_address = "233 5th Ave Ext, Johnstown NY 12095",            u_email = "Kasen.Williams@outlook.com"      },
            new User() { u_id = 3, u_name = "Hunter Morris",    u_regDate = DateTime.Parse("2015. 08. 01. 2:20:15"),  u_address = "374 William S Canning Blvd, Fall River MA 2721", u_email = "Hunter.Morris@outlook.com"       },
            new User() { u_id = 4, u_name = "Kathryn Diaz",     u_regDate = DateTime.Parse("2013. 04. 11. 18:50:49"), u_address = "295 Plymouth Street, Halifax MA 2338",           u_email = "Kathryn.Diaz@mail.ru"            },
            new User() { u_id = 5, u_name = "Stephany Bullock", u_regDate = DateTime.Parse("2019. 07. 08. 1:14:28"),  u_address = "279 Troy Road, East Greenbush NY 12061",         u_email = "Stephany.Bullock@protonmail.com" }
        };

    private void TestUserCreation()
    {

    }


        [Test]
        public void TestGetBook()
        {
            //Arrange
            Mock<IBookRepository> mockedBookRepo = new Mock<IBookRepository>();
            Mock<ICartItemRepository> mockedCartItemRepo = new Mock<ICartItemRepository>();
            Mock<ICartRepository> mockedCartRepo = new Mock<ICartRepository>();
            Mock<IPublisherRepository> mockedPublisherRepo = new Mock<IPublisherRepository>();

            BookLogic testlogic = new BookLogic(mockedBookRepo.Object, mockedCartItemRepo.Object, mockedCartRepo.Object, mockedPublisherRepo.Object);

            int id = 1;

            //Act
            testlogic.GetBook(id);

            //Assert
            mockedBookRepo.Verify(repo => repo.GetOneById(id), Times.Once());
        }

        //[Test]
        //public void TestUpdateUser()
        //{
        //    //Arrange
        //    Mock<IUserRepository> userRepo = new Mock<IUserRepository>();
        //    Mock<ICartRepository> cartRepo = new Mock<ICartRepository>();
        //    Mock<ICartItemRepository> cartItemRepo = new Mock<ICartItemRepository>();
        //    userRepo.Setup(repo => repo.UpdateName(It.IsAny<int>(), It.IsAny<string>()));
        //    UserLogic logic = new UserLogic(userRepo.Object, cartRepo.Object, cartItemRepo.Object);

        //    //Act
        //    logic.ChangeUser(It.IsAny<int>(), "John Smith");

        //    //Assert
        //    userRepo.Verify(repo => repo.UpdateName(It.IsAny<int>(), "John Smith"), Times.Once);
        //}

        [Test]
        public void TestAddPublisher()
        {
            //Arrange
            Mock<IPublisherRepository> publisherRepo = new Mock<IPublisherRepository>();
            Mock<IBookRepository> bookRepo = new Mock<IBookRepository>();
            publisherRepo.Setup(repo => repo.Insert(It.IsAny<Publisher>()));
            PublisherLogic logic = new PublisherLogic(publisherRepo.Object, bookRepo.Object);
            Publisher testAddPublisher = new Publisher { 
                                                            p_id = 1, p_name = "Good Book Publisher",
                                                            p_address = "nowhere 6",
                                                            p_email = "support@goodpublisher.com",
                                                            p_website = "goodpublisher.com" 
                                                        };

            //Act
            logic.AddNewPublisher(testAddPublisher);

            //Assert
            publisherRepo.Verify(repo => repo.Insert(It.IsAny<Publisher>()), Times.Once);
        }


        [Test]
        public void TestDeleteBook()
        {
            //What should I test?
            //Arrange
            Mock<IBookRepository> bookRepo = new Mock<IBookRepository>();
            Mock<ICartRepository> cartRepo = new Mock<ICartRepository>();
            Mock<ICartItemRepository> cartItemRepo = new Mock<ICartItemRepository>();
            Mock<IPublisherRepository> publisherRepo = new Mock<IPublisherRepository>();

            List<Book> bookList = new List<Book>()
            {
                new Book() { b_id = 1, b_title = "Watch of mystery", b_author = "Arden Wolfwood", b_price = 1000, b_releaseDate = DateTime.Parse("1983. 06. 09. 0:00:00"), b_publisher_id = 1},
                new Book() { b_id = 2, b_title = "Turtles and heroes", b_author = "Brooke Breathless", b_price = 1900, b_releaseDate = DateTime.Parse("2007. 08. 28. 0:00:00"), b_publisher_id = 2},
                new Book() { b_id = 3, b_title = "Songs in the catacombs", b_author = "Xander Marquis", b_price = 3800, b_releaseDate = DateTime.Parse("2019. 07. 21. 0:00:00"), b_publisher_id = 1},
            };
            List<CartItem> cartItemList = new List<CartItem>()
            {
                new CartItem() { ci_id = 1, ci_book_id = 2, ci_quantity = 3, ci_cart_id = 1 },
                new CartItem() { ci_id = 2, ci_book_id = 1, ci_quantity = 3, ci_cart_id = 2 },
                new CartItem() { ci_id = 3, ci_book_id = 1, ci_quantity = 2, ci_cart_id = 3 }
            };
            List<Cart> CartList = new List<Cart>()
            {
                new Cart() { c_id = 1, c_billingAddress = "2041 Douglas Avenue, Brewton AL 36426", c_creditcardNumber = "374602027947747", c_deliver = false, c_user_id = 2 },
                new Cart() { c_id = 2, c_billingAddress = "85 Crooked Hill Road, Commack NY 11725", c_creditcardNumber = "374602027947747", c_deliver = false, c_user_id = 2 },
                new Cart() { c_id = 3, c_billingAddress = "3176 South Eufaula Avenue, Eufaula AL 36027", c_creditcardNumber = "2720059742859433", c_deliver = true, c_user_id = 1 }
            };

            bookRepo.Setup(repo => repo.Remove(It.IsAny<int>()));
            BookLogic logic = new BookLogic(bookRepo.Object, cartItemRepo.Object, cartRepo.Object, publisherRepo.Object);

            //Act
            logic.DeleteBook(3);

            //Assert
            bookRepo.Verify(repo => repo.Remove(3), Times.Once);

        }
        

    }
}
