using Microsoft.VisualStudio.TestTools.UnitTesting;
using AskApp.DAL;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using AskApp.Cross_Cutting;

namespace AskApp.DAL.Tests
{
    [TestClass]
    public class UserRepositoryTests
    {
        public void Init()
        {
        }

        [TestMethod]
        public void TestInsert()
        {
            var option = new DbContextOptionsBuilder<AskAppContext>()
            .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
            .Options;

            var Michou = new UserEntity()
            {
                Name = "Michou",
                Role = UserRole.User
            };

            using (var context = new AskAppContext(option))
            {
                var repo = new UserRepository(context);

                Assert.AreEqual(0, repo.GetAll().Count);

                repo.Insert(Michou);
                context.SaveChanges();

                Assert.AreEqual(1, repo.GetAll().Count);
            }
        }

        [TestMethod]
        public void TestUpdate()
        {
            var option = new DbContextOptionsBuilder<AskAppContext>()
                        .UseInMemoryDatabase(databaseName: MethodBase.GetCurrentMethod().Name)
                        .Options;

            var Michou = new UserEntity()
            {
                Name = "Michou",
                Role = UserRole.User
            };

            using (var context = new AskAppContext(option))
            {
                var repo = new UserRepository(context);

                repo.Insert(Michou);
                context.SaveChanges();

                Assert.AreEqual("Michou", repo.GetById(1).Name);
                Michou.Name = "Jean-Luc";

                repo.Update(Michou);
                context.SaveChanges();
                Assert.AreEqual("Jean-Luc", repo.GetById(1).Name);
            }
        }
    }
}