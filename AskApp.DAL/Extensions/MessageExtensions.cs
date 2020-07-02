using AskApp.Cross_Cutting.TransferObjects;
using System;
using System.Collections.Generic;
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
    }
}