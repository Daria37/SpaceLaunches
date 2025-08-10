using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Rocket
{
    public class CreateRocketRequestDto
    {
        // [Column("ID")]
        public int ID { get; set; }
        // [Column("Name")]
        public required string Name { get; set; }
        // // [Column("Config")]
        // public required string Config { get; set; }
        // [Column("AgencyID")]
        public int AgencyID { get; set; }
    }
}