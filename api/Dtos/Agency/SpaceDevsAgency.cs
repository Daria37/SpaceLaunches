using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using api.Dtos.Launches;
using Newtonsoft.Json;

namespace api.Dtos.Agency
{
    public class SpaceDevsAgency
    {
        // public string response_mode { get; set; }
        [JsonProperty("id")]
        public int id { get; set; }
        // public string url { get; set; }
        // public string name { get; set; }
        // public string abbrev { get; set; }
        // // public Type type { get; set; }
        // public bool featured { get; set; }
        // public SpaceDevsCountry country { get; set; }
        // public string description { get; set; }
        // public string administrator { get; set; }
        // public int founding_year { get; set; }
        // public string launchers { get; set; }
        // public string spacecraft { get; set; }
        // public object parent { get; set; }
        // public object image { get; set; }
        // public Logo logo { get; set; }
        // public SocialLogo social_logo { get; set; }
    }
}