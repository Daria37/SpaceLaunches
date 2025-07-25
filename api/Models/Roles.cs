using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Roles
    {
        public int ID { get; set; }
        public required string Name { get; set; }

        // public List<Users> Users { get; set; } = new List<Users>();
    }
}