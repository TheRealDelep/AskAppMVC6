using AskApp.Cross_Cutting.TransferObjects;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace AskApp.DAL.Extensions
{
    public static class ThreadExtensions
    {
        public static ThreadTO ToTO(this ThreadEntity thread)
        {
            return new ThreadTO()
            {
                Id = thread.Id,
                Question = thread.Question.ToTO(),
                Comments = thread.Comments.Select(x => x.ToTO()).ToList(),
                IsClosed = thread.IsClosed
            };
        }

        public static ThreadEntity ToEntity(this ThreadTO thread)
        {
            return new ThreadEntity()
            {
                Id = thread.Id,
                Question = thread.Question.ToEntity(),
                Comments = thread.Comments.Select(x => x.ToEntity()).ToList(),
                IsClosed = thread.IsClosed
            };
        }
    }
}