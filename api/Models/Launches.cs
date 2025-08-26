using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class Launches
    {
        public required string ID { get; set; }
        public required string Name { get; set; }
        public string Image { get; set; }
        public required string Status { get; set; }
        public DateTime? CreatedOnUTC { get; set; }
        public required string MapImage { get; set; }
        public required string Mission { get; set; }
        public string? RocketName { get; set; }
        public string? CountryCode { get; set; }
        public Rocket? Rocket { get; set; }
    }
}