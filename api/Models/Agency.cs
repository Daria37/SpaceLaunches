using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Agency
    {
        public int ID { get; set; }
        public required string Name { get; set; }
        public required string Type { get; set; }
        public string? CountryCode { get; set; }
        // public List<Rocket> Rocket { get; set; } = new List<Rocket>();
        // public List<Launches> Launches { get; set; } = new List<Launches>();
    }
}