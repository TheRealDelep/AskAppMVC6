using Microsoft.VisualStudio.TestTools.UnitTesting;
using AskApp.DAL;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using AskApp.Cross_Cutting;
using AskApp.Cross_Cutting.TransferObjects;
using System;
using System.Linq;

namespace AskApp.DAL.Tests
{
    [TestClass]
    public class UserRepositoryTests
    {
        [TestMethod]
        public void IntegrationTest()
        {
            var option = new DbContextOptionsBuilder<AskAppContext>()
            .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
            .Options;

            var Michou = new UserTO()
            {
                Name = "Michou",
                Role = UserRole.User
            };

            using (var context = new AskAppContext(option))
            {
                var repo = new UserRepository(context);

                foreach (var item in repo.GetAll())
                {
                    repo.Delete(item);
                }
                context.SaveChanges();

                Assert.AreEqual(0, repo.GetAll().Count);

                // Testing Insert
                Michou = repo.Insert(Michou);
                context.SaveChanges();

                Assert.AreEqual(1, repo.GetAll().Count);

                //Testing Update
                Assert.AreEqual("Michou", repo.GetById(Michou.Id).Name);
                Michou.Name = "Jean-Luc";
                Michou.Role = UserRole.Teacher;

                repo.Update(Michou);
                context.SaveChanges();

                Assert.AreEqual("Jean-Luc", repo.GetById(Michou.Id).Name);
                Assert.AreEqual(UserRole.Teacher, repo.GetById(Michou.Id).Role);

                //Testing Delete
                repo.Delete(Michou);
                context.SaveChanges();
                Assert.AreEqual(0, repo.GetAll().Count);
            }
        }

        [TestMethod]
        public void DefaultUserTests()
        {
            var option = new DbContextOptionsBuilder<AskAppContext>()
            .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
            .Options;

            using (var context = new AskAppContext(option))
            {
                var userRepo = new UserRepository(context);

                // Create default user if doesn't already exists
                int defaultUserCount = context.Users.Where(x => x.Role == UserRole.Guest).Count();
                Assert.AreEqual(0, defaultUserCount);

                var defaultUser = userRepo.GetDefaultUser();
                context.SaveChanges();
                Assert.AreEqual(UserTO.DefaultUser.Name, defaultUser.Name);

                // Do not insert default user twice
                var defaultUser2 = userRepo.Insert(UserTO.DefaultUser);
                context.SaveChanges();

                Assert.AreEqual(defaultUser.Id, defaultUser2.Id);
            }
        }
    }
}