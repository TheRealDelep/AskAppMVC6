using System.Collections.Generic;

namespace AskApp.DAL
{
    public class ThreadEntity
    {
        public int Id { get; set; }
        public MessageEntity Question { get; set; }
        public List<MessageEntity> Comments { get; set; }
        public bool IsClosed { get; set; }
    }
}