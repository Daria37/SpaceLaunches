using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Agency
{
    public class AgencyDto
    {
        public int ID { get; set; }
        public required string Name { get; set; }
        public required string Type { get; set; }
        public string CountryCode { get; set; }
    }
}