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

        private void TestData()
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
        public void TestGetOneCartItem()
        {
            //Arrange
            Mock<ICartItemRepository> cartItemRepo = new Mock<ICartItemRepository>();
            List<CartItem> buyerList = new List<CartItem>()
            {
                new CartItem() { ci_id = 1, ci_book_id = 9, ci_cart_id = 5, ci_quantity = 1 },
                new CartItem() { ci_id = 2, ci_book_id = 8, ci_cart_id = 6, ci_quantity = 4 },
                new CartItem() { ci_id = 3, ci_book_id = 1, ci_cart_id = 7, ci_quantity = 2 },
            };
            cartItemRepo.Setup(repo => repo.GetOneById(It.IsAny<int>())).Returns(buyerList[1]);
            CartItemLogic logic = new CartItemLogic(cartItemRepo.Object);

            //Act
            var result = logic.GetCartItem(2);

            //Assert
            Assert.That(result.ci_id, Is.EqualTo(2));
            cartItemRepo.Verify(repo => repo.GetOneById(2), Times.Once);
        }

        [Test]
        public void TestCartGetBooksLogic()
        {
            //Arrange
            Mock<IBookRepository> bookRepo = new Mock<IBookRepository>();
            Mock<ICartRepository> cartRepo = new Mock<ICartRepository>();
            Mock<ICartItemRepository> cartItemRepo = new Mock<ICartItemRepository>();
            IEnumerable<Book> bookList = new List<Book>()
            {
                new Book() { b_id = 1, b_title = "Watch of mystery", b_author = "Arden Wolfwood", b_price = 1000, b_releaseDate = DateTime.Parse("1983. 06. 09. 0:00:00"), b_publisher_id = 1},
                new Book() { b_id = 2, b_title = "Turtles and heroes", b_author = "Brooke Breathless", b_price = 1900, b_releaseDate = DateTime.Parse("2007. 08. 28. 0:00:00"), b_publisher_id = 2},
                new Book() { b_id = 3, b_title = "Songs in the catacombs", b_author = "Xander Marquis", b_price = 3800, b_releaseDate = DateTime.Parse("2019. 07. 21. 0:00:00"), b_publisher_id = 1},
            };
            IEnumerable<Cart> CartList = new List<Cart>()
            {
                new Cart() { c_id = 1, c_billingAddress = "2041 Douglas Avenue, Brewton AL 36426", c_creditcardNumber = "374602027947747", c_deliver = false, c_user_id = 2 },
                new Cart() { c_id = 2, c_billingAddress = "85 Crooked Hill Road, Commack NY 11725", c_creditcardNumber = "374602027947747", c_deliver = false, c_user_id = 2 },
                new Cart() { c_id = 3, c_billingAddress = "3176 South Eufaula Avenue, Eufaula AL 36027", c_creditcardNumber = "2720059742859433", c_deliver = true, c_user_id = 1 }
            };
            IEnumerable<CartItem> cartItemList = new List<CartItem>()
            {
                new CartItem() { ci_id = 1, ci_book_id = 2, ci_quantity = 3, ci_cart_id = 1 },
                new CartItem() { ci_id = 2, ci_book_id = 1, ci_quantity = 3, ci_cart_id = 2 },
                new CartItem() { ci_id = 3, ci_book_id = 1, ci_quantity = 2, ci_cart_id = 1 }
            };
            bookRepo.Setup(repo => repo.GetAll()).Returns(bookList.AsQueryable());
            cartRepo.Setup(repo => repo.GetAll()).Returns(CartList.AsQueryable());
            cartItemRepo.Setup(repo => repo.GetAll()).Returns(cartItemList.AsQueryable());
            CartLogic logic = new CartLogic(cartRepo.Object, cartItemRepo.Object, bookRepo.Object );

            var test=logic.GetBooksInThisCart(1);
            Console.ReadKey();
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
