using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace api.Dtos.Agency
{
    public class SpaceDevsAgency
    {
        [JsonProperty("id")]
        public int id { get; set; }
    }
}