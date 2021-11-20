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
        public void TestReadBook()
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


    }
}
