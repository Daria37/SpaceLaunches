using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Roles;
using api.Models;

namespace api.Mappers
{
    public  static class RolesMappers
    {
        public static RolesDto ToRolesDto(this Roles rolesModel)
        {
            return new RolesDto
            {
                ID = rolesModel.ID,
                Name = rolesModel.Name
            };
        }
    }
}