using AskApp.Cross_Cutting;
using AskApp.DAL;
using AskApp.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AskApp.BLL
{
    public partial class UserRole
    {
        private AskAppContext context;

        private IRepository<ThreadEntity> threadRepo;
        private IRepository<MessageEntity> messageRepo;

        public UserRole()
        {
            threadRepo = new ThreadRepository(context);
            messageRepo = new MessageRepository(context);
        }

        public List<ThreadEntity> GetQuestionsByDate()
        {
            using (context)
            {
                return threadRepo.GetAll().OrderBy(x => x.Question.Date).ToList();
            }
        }
    }
}