using AskApp.Cross_Cutting.TransferObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AskApp.UI.Models
{
    public class ThreadDetailsVM
    {
        public ThreadTO Thread { get; set; }
        public MessageTO Comment { get; set; }
    }
}