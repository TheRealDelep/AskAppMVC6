using System;

namespace AskApp.Cross_Cutting.TransferObjects
{
    public class MessageTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public UserTO Author { get; set; }
        public DateTime Date { get; set; }
    }
}