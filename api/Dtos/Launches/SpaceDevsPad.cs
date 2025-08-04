using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace api.Dtos.Launches
{
    public class SpaceDevsPad
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("country_code")]
        public string country { get; set; }
    }
}