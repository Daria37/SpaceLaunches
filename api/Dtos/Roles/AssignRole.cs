using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Roles
{
    public class AssignRoleDto
    {
        public string UserId { get; set; }
        public string RoleName { get; set; }
    }
}