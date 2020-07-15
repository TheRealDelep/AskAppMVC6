using Microsoft.VisualStudio.TestTools.UnitTesting;
using AskApp.DAL;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using AskApp.Cross_Cutting;
using AskApp.DAL.Repositories;
using System.Threading;
using Moq;
using System.Collections.Generic;
using System;
using AskApp.Cross_Cutting.TransferObjects;

namespace AskApp.DAL.Tests
{
    [TestClass]
    public class MessageRepoTests
    {
        [TestMethod]
        public void IntegrationTest()
        {
            var option = new DbContextOptionsBuilder<AskAppContext>()
            .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
            .EnableSensitiveDataLogging()
            .Options;

            var message = new MessageTO()
            {
                Author = new Mock<UserTO>().Object,
                Title = "Why is this so redundant?",
                Body = "I mean, this exercice is supposed to be about MVC, not app architecture",
                Date = DateTime.Now
            };

            using (var context = new AskAppContext(option))
            {
                var repo = new MessageRepository(context);

                // Test Insert

                Assert.AreEqual(0, repo.GetAll().Count);
                message = repo.Insert(message);
                context.SaveChanges();

                Assert.AreEqual(1, repo.GetAll().Count);

                // Test Update

                string bodyUpdated = "But I guess I'll stick to it anyway";
                message.Body = bodyUpdated;
                repo.Update(message);
                context.SaveChanges();

                Assert.AreEqual(bodyUpdated, repo.GetById(1).Body);

                // Test Delete

                repo.Delete(message);
                context.SaveChanges();
                Assert.AreEqual(0, repo.GetAll().Count);
            }
        }
    }
}