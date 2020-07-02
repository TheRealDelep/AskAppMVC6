using System;

namespace AskApp.DAL
{
    public class MessageEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public UserEntity Author { get; set; }
        public DateTime Date { get; set; }
    }
}