using AskApp.Cross_Cutting;
using AskApp.Cross_Cutting.Interfaces;
using AskApp.Cross_Cutting.TransferObjects;
using AskApp.DAL;
using AskApp.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AskApp.BLL
{
    public partial class UserUseCases
    {
        private AskAppContext context;

        private IRepository<ThreadTO> threadRepo;
        private IRepository<MessageTO> messageRepo;
        private IUserRepository userRepo;

        public UserUseCases()
        {
            context = new AskAppContext();

            threadRepo = new ThreadRepository(context);
            messageRepo = new MessageRepository(context);
            userRepo = new UserRepository(context);
        }

        public List<ThreadTO> GetQuestionsByDate()
        {
            return threadRepo.GetAll().OrderBy(x => x.Question.Date).Reverse().ToList();
        }

        public ThreadTO GetByID(int Id)
        {
            return threadRepo.GetById(Id);
        }

        public ThreadTO CreateThread(ThreadTO thread)
        {
            if (thread.Question == null || string.IsNullOrEmpty(thread.Question.Body))
            {
                throw new ArgumentException("Thread must contain a question");
            }
            if (thread.Question.Author == null)
            {
                thread.Question.Author = userRepo.GetDefaultUser();
            }
            thread.Question.Date = DateTime.Now;

            var result = threadRepo.Insert(thread);
            return result;
        }

        public ThreadTO Reply(ThreadTO thread, MessageTO reply)
        {
            if (thread.Question == null || string.IsNullOrEmpty(thread.Question.Body))
            {
                throw new ArgumentException("Thread must contain a question");
            }
            if (reply.Author == null)
            {
                reply.Author = userRepo.GetDefaultUser();
            }

            reply.Date = DateTime.Now;
            reply = messageRepo.Insert(reply);

            thread.Comments.Add(reply);
            threadRepo.Update(thread);
            return threadRepo.GetById(thread.Id);
        }
    }
}