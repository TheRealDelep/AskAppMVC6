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
            var result = new ThreadTO()
            {
                Id = thread.Id,
                Question = thread.Question.ToTO(),
                IsClosed = thread.IsClosed
            };

            if (thread.Comments != null)
            {
                result.Comments = thread.Comments.ToTO();
            }

            return result;
        }

        public static ThreadEntity ToEntity(this ThreadTO thread)
        {
            var result = new ThreadEntity()
            {
                Id = thread.Id,
                Question = thread.Question.ToEntity(),
                IsClosed = thread.IsClosed
            };

            if (thread.Comments != null)
            {
                result.Comments = thread.Comments.ToEntity();
            }

            return result;
        }

        public static void UpdateFromDetached(this ThreadEntity attached, ThreadEntity dettached)
        {
            attached.Question = dettached.Question;
            attached.Comments = dettached.Comments;
            attached.IsClosed = dettached.IsClosed;
        }
    }
}