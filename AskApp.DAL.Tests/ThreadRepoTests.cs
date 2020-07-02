using Microsoft.VisualStudio.TestTools.UnitTesting;
using AskApp.DAL;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using AskApp.Cross_Cutting;
using AskApp.DAL.Repositories;
using System.Threading;
using Moq;
using System.Collections.Generic;

namespace AskApp.DAL.Tests
{
    [TestClass]
    public class ThreadRepoTests
    {
        [TestMethod]
        public void IntegrationTest()
        {
            var option = new DbContextOptionsBuilder<AskAppContext>()
            .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
            .Options;

            var subject = new ThreadEntity()
            {
                Question = new Mock<MessageEntity>().Object,
                Comments = new Mock<List<MessageEntity>>().Object,
                IsClosed = false
            };

            using (var context = new AskAppContext(option))
            {
                var repo = new ThreadRepository(context);
                Assert.AreEqual(0, repo.GetAll().Count);

                // Testing Insert
                repo.Insert(subject);
                context.SaveChanges();
                Assert.AreEqual(1, repo.GetAll().Count);

                // Testing Update
                subject.IsClosed = true;
                repo.Update(subject);
                context.SaveChanges();

                Assert.IsTrue(repo.GetById(1).IsClosed);

                // Testing Delete
                repo.Delete(subject);
                context.SaveChanges();
                Assert.AreEqual(0, repo.GetAll().Count);
            }
        }
    }
}