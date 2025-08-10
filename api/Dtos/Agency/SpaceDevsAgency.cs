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
        [JsonProperty("name")]
        public required string Name { get; set; }
        [JsonProperty("type")]
        public required string Type { get; set; }
        [JsonProperty("country_code")]
        public string? CountryCode { get; set; }
    }
}