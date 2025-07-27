using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Roles
{
    public class RolesDto
    {
        public int ID { get; set; }
        public required string Name { get; set; }
    }
}