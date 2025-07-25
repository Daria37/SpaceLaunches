using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("rocket")]
    public class Rocket
    {
        [Column("ID")]
        public int ID { get; set; }
        [Column("Name")]
        public required string Name { get; set; }
        [Column("Config")]
        public required string Config { get; set; }
        [Column("AgencyID")]
        public int AgencyID { get; set; }

        // public List<Launches> Lauches { get; set; } = new List<Launches>();
    }
}