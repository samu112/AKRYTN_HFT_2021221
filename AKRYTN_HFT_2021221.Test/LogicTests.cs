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
        public void TestDeleteBook()
        {
            Mock<IBookRepository> mockedRepo = new Mock<IBookRepository>();

            BookLogic testlogic = new BookLogic(mockedRepo.Object);

            int id = 1;

            testlogic.DeleteBook(id);

            mockedRepo.Verify(repo => repo.Remove(id), Times.Once());
        }
    }
}
