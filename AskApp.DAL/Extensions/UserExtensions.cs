using AskApp.Cross_Cutting.TransferObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace AskApp.DAL.Extensions
{
    public static class UserExtensions
    {
        public static UserTO ToTO(this UserEntity user)
        {
            return new UserTO()
            {
                Id = user.Id,
                Name = user.Name,
                Role = user.Role
            };
        }

        public static UserEntity ToEntity(this UserTO user)
        {
            return new UserEntity()
            {
                Id = user.Id,
                Name = user.Name,
                Role = user.Role
            };
        }
    }
}