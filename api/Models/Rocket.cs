using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    [Table("Rocket")]
    public class Rocket
    {
        [Column("ID")]
        public int ID { get; set; }
        [Column("Name")]
        public required string Name { get; set; }
        [Column("AgencyID")]
        public int AgencyID { get; set; }
        // [Column("Configuration")]
        // public string Configuration { get; set; }

        public List<Launches> Lauches { get; set; } = new List<Launches>();
    }
}