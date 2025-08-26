using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Launches
{
    public class LaunchesDto
    {
        public required string ID { get; set; }
        public required string Name { get; set; }
        public string Image { get; set; }
        public string Status { get; set; }
        public DateTime CreatedOnUTC { get; set; }
        public string RocketName { get; set; }
        public string Mission { get; set; }
        public string? CountryCode { get; set; }
    }
}