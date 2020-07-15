using AskApp.Cross_Cutting;
using AskApp.Cross_Cutting.TransferObjects;
using AskApp.DAL.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AskApp.DAL.Repositories
{
    public class ThreadRepository : IRepository<ThreadTO>
    {
        private AskAppContext context;

        public ThreadRepository(AskAppContext context)
        {
            this.context = context;
        }

        public void Delete(ThreadTO entity)
        {
            var attached = context.Threads.Find(entity.Id);
            context.Threads.Remove(attached);
        }

        public List<ThreadTO> GetAll()
        {
            try
            {
                return context.Threads
                              .Include(x => x.Question)
                              .ThenInclude(x => x.Author)
                              .Include(x => x.Comments)
                              .ThenInclude(x => x.Author)
                              .Select(x => x.ToTO()).ToList();
            }
            catch (NullReferenceException)
            {
                return new List<ThreadTO>();
            }
        }

        public ThreadTO GetById(int Id)
            => context.Threads
                      .Include(x => x.Question)
                      .ThenInclude(x => x.Author)
                      .Include(x => x.Comments)
                      .ThenInclude(x => x.Author)
                      .AsNoTracking()
                      .FirstOrDefault(x => x.Id == Id)
                      .ToTO();

        public ThreadTO Insert(ThreadTO entity)
        {
            var thread = entity.ToEntity();

            thread.Question.Author = context.Users.FirstOrDefault(x => x.Id == entity.Question.Author.Id);
            thread.Comments.ForEach(x => x.Author = context.Users.FirstOrDefault(y => y.Id == x.Author.Id));

            var result = context.Threads.Add(thread);
            context.SaveChanges();
            return GetById(result.Entity.Id);
        }

        public void Update(ThreadTO entity)
        {
            var attached = context.Threads.Find(entity.Id);
            attached.UpdateFromDetached(entity.ToEntity());

            attached.Question = context.Messages.Find(attached.Question.Id);

            //var timer = new Stopwatch();
            //timer.Start();

            // Time : 14.6149ms
            //attached.Comments = attached.Comments
            //    .Select(x => x = context.Messages
            //    .FirstOrDefault(y => y.Id == x.Id))
            //    .ToList();

            // Time : 0.1635 ms
            var comments = new List<MessageEntity>();
            attached.Comments.ForEach(x => comments.Add(context.Messages.Find(x.Id)));
            attached.Comments = comments;

            //timer.Stop();
            //var elapsedTime = timer.Elapsed.TotalMilliseconds;
            //Console.WriteLine(elapsedTime);

            context.Entry(attached).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}