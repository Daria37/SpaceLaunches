using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Users
    {
        public int ID { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public int? RolesID { get; set; }
        public Roles? Role { get; set; }
    }
}