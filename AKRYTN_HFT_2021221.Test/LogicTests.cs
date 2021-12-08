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
        private Mock<IUserRepository> mockedUserRepo = new Mock<IUserRepository>();

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

        //5 NON-CRUD tests

        [Test]
        public void TestGetPublisherOfBook()
        {
            //Arrange
            Mock<IBookRepository> bookRepo = new Mock<IBookRepository>();
            Mock<ICartItemRepository> cartItemRepo = new Mock<ICartItemRepository>();
            Mock<ICartRepository> cartRepo = new Mock<ICartRepository>();
            Mock<IPublisherRepository> publisherRepo = new Mock<IPublisherRepository>();

            bookRepo.Setup(repo => repo.GetOneById(It.IsAny<int>())).Returns(booksDb[2]);

            publisherRepo.Setup(repo=>repo.GetOneById(It.IsAny<int>())).Returns(publishersDb[2]);

            BookLogic booklogic = new BookLogic(bookRepo.Object, cartItemRepo.Object, cartRepo.Object, publisherRepo.Object);

            int id = 2;

            //Act
            Publisher getBooksPublisher = booklogic.GetBookPublisher(id);

            //Assert
            Assert.That(getBooksPublisher.p_id, Is.EqualTo(3));
            publisherRepo.Verify(repo => repo.GetOneById(3), Times.Once());
        }

        [Test]
        public void TestGetCartItemsInThisCart()
        {
            //Arrange
            Mock<ICartRepository> cartRepo = new Mock<ICartRepository>();
            Mock<ICartItemRepository> cartItemRepo = new Mock<ICartItemRepository>();
            Mock<IBookRepository> bookRepo = new Mock<IBookRepository>();
            Mock<IUserRepository> userRepo = new Mock<IUserRepository>();
            CartLogic cartLogic = new CartLogic(cartRepo.Object, cartItemRepo.Object, bookRepo.Object, userRepo.Object);

            int id = 2;

            cartItemRepo.Setup(repo => repo.GetAll()).Returns(cartItemsDb.AsQueryable());

            //Act
            var cartItems = cartLogic.GetCartItemsInThisCart(id);

            //Assert
            Assert.That(cartItems.Count(), Is.EqualTo(3));
            cartItemRepo.Verify(repo => repo.GetAll(), Times.Once());
        }

        [Test]
        public void TestGetCartPrice()
        {
            //Arrange
            Mock<ICartRepository> cartRepo = new Mock<ICartRepository>();
            Mock<ICartItemRepository> cartItemRepo = new Mock<ICartItemRepository>();
            Mock<IBookRepository> bookRepo = new Mock<IBookRepository>();
            Mock<IUserRepository> userRepo = new Mock<IUserRepository>();
            CartLogic cartLogic = new CartLogic(cartRepo.Object, cartItemRepo.Object, bookRepo.Object, userRepo.Object);

            cartItemRepo.Setup(repo => repo.GetAll()).Returns(cartItemsDb.AsQueryable());
            bookRepo.Setup(repo => repo.GetAll()).Returns(booksDb.AsQueryable());

            int id = 2;

            //Act
            double price = cartLogic.GetCartPrice(id);

            //Assert
            Assert.That(price, Is.EqualTo(19200));
            cartItemRepo.Verify(repo => repo.GetAll(), Times.Once());
            bookRepo.Verify(repo => repo.GetAll(), Times.Once());
        }

        [Test]
        public void GetBooksInThisCart()
        {
            //Arrange
            Mock<ICartRepository> cartRepo = new Mock<ICartRepository>();
            Mock<ICartItemRepository> cartItemRepo = new Mock<ICartItemRepository>();
            Mock<IBookRepository> bookRepo = new Mock<IBookRepository>();
            Mock<IUserRepository> userRepo = new Mock<IUserRepository>();
            CartLogic cartLogic = new CartLogic(cartRepo.Object, cartItemRepo.Object, bookRepo.Object, userRepo.Object);
            
            cartItemRepo.Setup(repo => repo.GetAll()).Returns(cartItemsDb.AsQueryable());

            int id = 3;

            //Act
            var books = cartLogic.GetBooksInThisCart(id);

            //Assert
            Assert.That(books.Count, Is.EqualTo(3));
            cartItemRepo.Verify(repo => repo.GetAll(), Times.Once());
            bookRepo.Verify(repo => repo.GetOneById(It.IsAny<int>()), Times.Exactly(3));
        }





        //3 Creatation Tests

        [Test]
        public void TestAddPublisherWihtMissingRequiredField()
        {
            //Arrange
            Mock<IPublisherRepository> publisherRepo = new Mock<IPublisherRepository>();
            Mock<IBookRepository> bookRepo = new Mock<IBookRepository>();
            PublisherLogic logic = new PublisherLogic(publisherRepo.Object, bookRepo.Object);
            Publisher testAddPublisher = new Publisher { 
                                                            p_id = 1, p_name = "Good Book Publisher",
                                                            //p_address = "nowhere 6",
                                                            p_email = "support@goodpublisher.com",
                                                            p_website = "goodpublisher.com" 
                                                        };
            //Act+Assert
            Assert.Throws<System.ArgumentNullException>(() => logic.AddNewPublisher(testAddPublisher));
        }

        [Test]
        public void TestAddPublisherSuccess()
        {
            //Arrange
            Mock<IPublisherRepository> publisherRepo = new Mock<IPublisherRepository>();
            Mock<IBookRepository> bookRepo = new Mock<IBookRepository>();
            publisherRepo.Setup(repo => repo.Insert(It.IsAny<Publisher>()));
            PublisherLogic logic = new PublisherLogic(publisherRepo.Object, bookRepo.Object);
            Publisher testAddPublisher = new Publisher
            {
                p_id = 1,
                p_name = "Good Book Publisher",
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
        public void TestAddCartItemWitNonExisingBook()
        {
            //Arrange
            Mock<ICartItemRepository> cartItemRepo = new Mock<ICartItemRepository>();
            Mock<IBookRepository> bookRepo = new Mock<IBookRepository>();
            Mock<ICartRepository> cartRepo = new Mock<ICartRepository>();

            CartItemLogic logic = new CartItemLogic(cartItemRepo.Object, bookRepo.Object, cartRepo.Object);
            CartItem testAddCartItem = new CartItem
            {
                ci_book_id = 12,
                ci_cart_id = 1,
                ci_quantity =2
            };
            //Act+Assert
            Assert.Throws<System.ArgumentException>(() => logic.AddNewCartItem(testAddCartItem));
        }

        //2 freely chosen test

        [Test]
        public void TestDeleteUserSuccess()
        {
            //Arrange
            Mock<IBookRepository> bookRepo = new Mock<IBookRepository>();
            Mock<ICartRepository> cartRepo = new Mock<ICartRepository>();
            Mock<ICartItemRepository> cartItemRepo = new Mock<ICartItemRepository>();
            Mock<IUserRepository> userRepo = new Mock<IUserRepository>();
            
            List<User> userList = new List<User>()
            {
                new User() { u_id = 1, u_name = "Charlotte Reilly", u_regDate = DateTime.Parse("2014. 06. 30. 20:01:28"), u_address = "333 Main Street, Tewksbury MA 1876", u_email = "Charlotte.Reilly@outlook.com" },
                new User() { u_id = 2, u_name = "Aimee Rocha", u_regDate = DateTime.Parse("2010. 10. 01. 15:47:57"), u_address = "311 RT 9W, Glenmont NY 12077", u_email = "Aimee.Rocha@protonmail.com" },
                new User() { u_id = 3, u_name = "Clark Singleton", u_regDate = DateTime.Parse("2015. 12. 04. 18:49:42"), u_address = "3018 East Ave, Central Square NY 13036", u_email = "Clark.Singleton@mail.ru" },
            };

            userRepo.Setup(repo => repo.GetAll()).Returns(userList.AsQueryable());
            UserLogic logic = new UserLogic(userRepo.Object, cartRepo.Object, cartItemRepo.Object, bookRepo.Object);

            //Act
            logic.DeleteUser(1);

            //Assert
            userRepo.Verify(repo => repo.Remove(1), Times.Once);

        }

        [Test]
        public void TestGetBookWithNonExistentId()
        {
            //Arrange
            Mock<IBookRepository> bookRepo = new Mock<IBookRepository>();
            Mock<ICartItemRepository> cartItemRepo = new Mock<ICartItemRepository>();
            Mock<ICartRepository> cartRepo = new Mock<ICartRepository>();
            Mock<IPublisherRepository> publisherRepo = new Mock<IPublisherRepository>();

            List<Book> bookList = new List<Book>() {
                new Book() { b_id = 1, b_title = "How to become Rich!", b_author = "Rich Richard", b_price = 25000, b_releaseDate = DateTime.Parse("2021.02.01 00:00:00"), b_publisher_id = 1 },
                new Book() { b_id = 2, b_title = "How to become Poor!", b_author = "Poor Paul", b_price = 10, b_releaseDate = DateTime.Parse("2017.09.12 00:00:00"), b_publisher_id = 1 },
                new Book() { b_id = 3, b_title = "How to get a book succesfully!", b_author = "Succesfull Sam", b_price = 999999, b_releaseDate = DateTime.Parse("2021.12.06 00:00:00"), b_publisher_id = 1 }
            };

            //bookRepo.Setup(repo => repo.GetOneById(3)).Returns(bookList.AsQueryable());
            BookLogic booklogic = new BookLogic(bookRepo.Object, cartItemRepo.Object, cartRepo.Object, publisherRepo.Object);

            int id = 999;

            //Act
            Book getBook = booklogic.GetBook(id);

            //Assert
            Assert.IsNull(getBook);
            bookRepo.Verify(repo => repo.GetOneById(id), Times.Once());
        }


    }
}
