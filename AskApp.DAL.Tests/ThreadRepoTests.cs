using Microsoft.VisualStudio.TestTools.UnitTesting;
using AskApp.DAL;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using AskApp.Cross_Cutting;
using AskApp.DAL.Repositories;
using System.Threading;
using Moq;
using System.Collections.Generic;
using AskApp.Cross_Cutting.TransferObjects;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace AskApp.DAL.Tests
{
    [TestClass]
    public class ThreadRepoTests
    {
        [TestMethod]
        public void CRUDTest()
        {
            var option = new DbContextOptionsBuilder<AskAppContext>()
            .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
            .Options;

            UserTO defaultUser;
            using (var context = new AskAppContext(option))
            {
                var userRepo = new UserRepository(context);
                defaultUser = userRepo.GetDefaultUser();
                context.SaveChanges();
            }

            var testQuestion = new MessageTO()
            {
                Author = defaultUser
            };

            var testComment = new List<MessageTO>()
            {
                new MessageTO()
                {
                    Author = defaultUser
                }
            };

            var subject = new ThreadTO()
            {
                Question = testQuestion,
                Comments = testComment,
                IsClosed = false
            };

            using (var context = new AskAppContext(option))
            {
                var threadRepo = new ThreadRepository(context);
                Assert.AreEqual(0, threadRepo.GetAll().Count);

                // Testing Insert
                subject = threadRepo.Insert(subject);
                Assert.AreEqual(1, threadRepo.GetAll().Count);

                // Testing Update
                subject.IsClosed = true;
                threadRepo.Update(subject);

                Assert.IsTrue(threadRepo.GetById(1).IsClosed);

                //Testing Includes
                var test = threadRepo.GetById(subject.Id);
                Assert.IsNotNull(test.Question.Author);

                // Testing Delete
                threadRepo.Delete(subject);
                context.SaveChanges();
                Assert.AreEqual(0, threadRepo.GetAll().Count);
            }
        }

        [TestMethod]
        public void Return_EmptyList_If_Table_Empty()
        {
            var option = new DbContextOptionsBuilder<AskAppContext>()
            .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
            .Options;

            using (var context = new AskAppContext(option))
            {
                var repo = new ThreadRepository(context);

                Assert.AreEqual(0, repo.GetAll().Count);
            }
        }
    }
}