using AskApp.Cross_Cutting.TransferObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace AskApp.Cross_Cutting.Interfaces
{
    public interface IUserRepository : IRepository<UserTO>
    {
        UserTO GetDefaultUser();
    }
}