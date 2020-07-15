using AskApp.Cross_Cutting.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AskApp.DAL.Extensions
{
    public static class MessageExtensions
    {
        public static MessageTO ToTO(this MessageEntity message)
        {
            return new MessageTO()
            {
                Id = message.Id,
                Author = message.Author.ToTO(),
                Title = message.Title,
                Body = message.Body,
                Date = message.Date
            };
        }

        public static MessageEntity ToEntity(this MessageTO message)
        {
            return new MessageEntity()
            {
                Id = message.Id,
                Author = message.Author.ToEntity(),
                Title = message.Title,
                Body = message.Body,
                Date = message.Date
            };
        }

        public static void UpdateFromDetached(this MessageEntity attached, MessageEntity dettached)
        {
            attached.Author = dettached.Author;
            attached.Title = dettached.Title;
            attached.Body = dettached.Body;
            attached.Date = dettached.Date;
        }

        public static List<MessageTO> ToTO(this List<MessageEntity> messages)
            => messages.Select(x => x.ToTO()).ToList();

        public static List<MessageEntity> ToEntity(this List<MessageTO> messages)
            => messages.Select(x => x.ToEntity()).ToList();
    }
}