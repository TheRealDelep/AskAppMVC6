using AskApp.Cross_Cutting;
using AskApp.Cross_Cutting.Interfaces;
using AskApp.Cross_Cutting.TransferObjects;
using AskApp.DAL.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AskApp.DAL
{
    public class UserRepository : IUserRepository
    {
        private AskAppContext context;

        public UserRepository(AskAppContext context)
        {
            this.context = context;
        }

        public UserTO GetDefaultUser()
        {
            try
            {
                return context.Users.FirstOrDefault(x => x.Role == UserRole.Guest).ToTO();
            }
            catch (NullReferenceException)
            {
                return Insert(UserTO.DefaultUser);
            }
        }

        public void Delete(UserTO entity)
        {
            var attached = context.Users.Find(entity.Id);
            context.Users.Remove(attached);
        }

        public List<UserTO> GetAll()
            => context.Users
                .AsNoTracking()
                .Select(x => x.ToTO())
                .ToList();

        public UserTO GetById(int Id)
            => context.Users
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == Id)
                .ToTO();

        public UserTO Insert(UserTO entity)
        {
            if (entity.Role == UserRole.Guest)
            {
                if (context.Users.FirstOrDefault(x => x.Role == UserRole.Guest) != null)
                {
                    return GetDefaultUser();
                }
            }
            return context.Users.Add(entity.ToEntity()).Entity.ToTO();
        }

        public void Update(UserTO entity)
        {
            var attached = context.Users.Find(entity.Id);
            attached.UpdateFromDetached(entity.ToEntity());
            context.Entry(attached).State = EntityState.Modified;
        }
    }
}