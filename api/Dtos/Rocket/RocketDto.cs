using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Dtos.Rocket
{
    [Table("rocket")]
    public class RocketDto
    {
        [Column("ID")]
        public int ID { get; set; }
        [Column("Name")]
        public required string Name { get; set; }
        [Column("Config")]
        public required string Config { get; set; }
        [Column("AgencyID")]
        public int AgencyID { get; set; }
    }
}