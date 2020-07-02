using AskApp.Cross_Cutting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace AskApp.DAL.Repositories
{
    public class ThreadRepository : IRepository<ThreadEntity>
    {
        private AskAppContext context;

        public ThreadRepository(AskAppContext context)
        {
            this.context = context;
        }

        public void Delete(ThreadEntity entity)
        {
            context.Threads.Remove(entity);
        }

        public List<ThreadEntity> GetAll()
        {
            return context.Threads.ToList();
        }

        public ThreadEntity GetById(int Id)
        {
            return context.Threads.Find(Id);
        }

        public void Insert(ThreadEntity entity)
        {
            context.Threads.Add(entity);
        }

        public void Update(ThreadEntity entity)
        {
            context.Threads.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}