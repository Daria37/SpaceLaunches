using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models;
using api.Dtos.Users;

namespace api.Mappers
{
    public static class UsersMappers
    {
        public static UsersDto ToUsersDto(this Users usersModel)
        {
            return new UsersDto
            {
                ID = usersModel.ID,
                Email = usersModel.Email
            };
        }
    }
}