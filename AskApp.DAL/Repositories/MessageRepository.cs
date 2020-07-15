using AskApp.Cross_Cutting;
using AskApp.Cross_Cutting.TransferObjects;
using AskApp.DAL.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AskApp.DAL.Repositories
{
    public class MessageRepository : IRepository<MessageTO>
    {
        private AskAppContext context;

        public MessageRepository(AskAppContext context)
        {
            this.context = context;
        }

        public void Delete(MessageTO entity)
        {
            var attached = context.Messages.Find(entity.Id);
            context.Messages.Remove(attached);
        }

        public List<MessageTO> GetAll()
            => context.Messages
                .Include(x => x.Author)
                .AsNoTracking()
                .Select(x => x.ToTO())
                .ToList();

        public MessageTO GetById(int Id)
            => context.Messages
                .AsNoTracking()
                .Include(x => x.Author)
                .FirstOrDefault(x => x.Id == Id)
                .ToTO();

        public MessageTO Insert(MessageTO entity)
        {
            var message = entity.ToEntity();

            message.Author = context.Users.FirstOrDefault(x => x.Id == entity.Author.Id);

            var result = context.Messages.Add(message);
            context.SaveChanges();
            return GetById(result.Entity.Id);
        }

        public void Update(MessageTO entity)
        {
            var attached = context.Messages.Find(entity.Id);
            attached.UpdateFromDetached(entity.ToEntity());
            attached.Author = context.Users.Find(attached.Author.Id);
            context.Entry(attached).State = EntityState.Modified;
        }
    }
}