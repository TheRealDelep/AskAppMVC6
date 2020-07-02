using AskApp.Cross_Cutting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AskApp.DAL.Repositories
{
    public class MessageRepository : IRepository<MessageEntity>
    {
        private AskAppContext context;

        public MessageRepository(AskAppContext context)
        {
            this.context = context;
        }

        public void Delete(MessageEntity entity)
        {
            context.Messages.Remove(entity);
        }

        public List<MessageEntity> GetAll()
        {
            return context.Messages.Include(x => x.Author).ToList();
        }

        public MessageEntity GetById(int Id)
        {
            return context.Messages.Include(x => x.Author).FirstOrDefault(x => x.Id == Id);
        }

        public void Insert(MessageEntity entity)
        {
            context.Messages.Add(entity);
        }

        public void Update(MessageEntity entity)
        {
            context.Messages.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}