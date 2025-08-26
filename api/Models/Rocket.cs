using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace api.Models
{
    public class Rocket
    {
        public int ID { get; set; }
        public required string Name { get; set; }
        public int AgencyID { get; set; }

        public List<Launches> Lauches { get; set; } = new List<Launches>();
    }
}