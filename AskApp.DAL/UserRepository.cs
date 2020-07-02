using AskApp.Cross_Cutting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AskApp.DAL
{
    class UserRepository : IRepository<UserEntity>
    {
        private AskAppContext context;

        public UserRepository(AskAppContext context)
        {
            this.context = context;
        }

        public void Delete(UserEntity entity)
        {
            if (GetById(entity.Id) != null)
            {
                context.Users.Remove(entity);
            }
            else
            {
                throw new ArgumentException("Cannot find item to delete");
            }
        }

        public List<UserEntity> GetAll()
        {
            return context.Users.ToList();
        }

        public UserEntity GetById(int Id)
        {
            return context.Users.Find(Id);
        }

        public void Insert(UserEntity entity)
        {
            context.Users.Add(entity);
        }

        public void Update(UserEntity entity)
        {
            context.Users.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}