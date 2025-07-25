using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Users
{
    public class UsersDto
    {
        public int ID { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}