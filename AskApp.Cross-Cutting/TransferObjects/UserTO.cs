using System;
using System.IO.Pipes;

namespace AskApp.Cross_Cutting.TransferObjects
{
    public class UserTO
    {
        public static readonly UserTO DefaultUser = new UserTO()
        {
            Name = "Anonymous",
            Role = UserRole.Guest
        };

        public int Id { get; set; }
        public string Name { get; set; }
        public UserRole Role { get; set; }
    }
}