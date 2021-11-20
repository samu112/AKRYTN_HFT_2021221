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
        [Test]
        public void TestGetBook()
        {
            //Arrange
            Mock<IBookRepository> mockedRepo = new Mock<IBookRepository>();

            BookLogic testlogic = new BookLogic(mockedRepo.Object);

            int id = 1;

            //Act
            testlogic.GetBook(id);

            //Assert
            mockedRepo.Verify(repo => repo.GetOneById(id), Times.Once());
        }

        [Test]
        public void TestUpdateUser()
        {
            //Arrange
            Mock<IUserRepository> userRepo = new Mock<IUserRepository>();
            userRepo.Setup(repo => repo.UpdateName(It.IsAny<int>(), It.IsAny<string>()));
            UserLogic logic = new UserLogic(userRepo.Object);

            //Act
            logic.ChangeUserName(It.IsAny<int>(), "John Smith");

            //Assert
            userRepo.Verify(repo => repo.UpdateName(It.IsAny<int>(), "John Smith"), Times.Once);
        }

        [Test]
        public void TestAddPublisher()
        {
            //Arrange
            Mock<IPublisherRepository> publisherRepo = new Mock<IPublisherRepository>();
            publisherRepo.Setup(repo => repo.Insert(It.IsAny<Publisher>()));
            PublisherLogic logic = new PublisherLogic(publisherRepo.Object);
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
        public void TestSomething()
        {
            //What should I test?
        }

    }
}
