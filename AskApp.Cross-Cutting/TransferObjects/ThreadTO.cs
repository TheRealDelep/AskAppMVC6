using System;
using System.Collections.Generic;
using System.Text;

namespace AskApp.Cross_Cutting.TransferObjects
{
    public class ThreadTO
    {
        public int Id { get; set; }
        public MessageTO Question { get; set; }
        public List<MessageTO> Comments { get; set; }
        public bool IsClosed { get; set; }
    }
}