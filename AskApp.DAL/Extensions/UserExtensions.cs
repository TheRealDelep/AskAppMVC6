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
            if (user == null)
            {
                throw new NullReferenceException();
            }

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

        public static void UpdateFromDetached(this UserEntity attached, UserEntity dettached)
        {
            attached.Id = dettached.Id;
            attached.Name = dettached.Name;
            attached.Role = dettached.Role;
        }
    }
}