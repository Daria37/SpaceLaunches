using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Rocket
{
    public class UpdateRocketRequestDto
    {
        public int ID { get; set; }
        public int AgencyID { get; set; }
    }
}