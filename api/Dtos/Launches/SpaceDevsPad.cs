using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Agency;
using Newtonsoft.Json;

namespace api.Dtos.Launches
{
    public class SpaceDevsPad
    {
        [JsonProperty("id")]
        public int id { get; set; }
        // public string url { get; set; }
        // public bool active { get; set; }
        // public List<SpaceDevsAgency> agencies { get; set; }
        // public string name { get; set; }
        // public object image { get; set; }
        // public object description { get; set; }
        // public object info_url { get; set; }
        // public string wiki_url { get; set; }
        // public string map_url { get; set; }
        // public double latitude { get; set; }
        // public double longitude { get; set; }
        [JsonProperty("country_code")]
        public string country { get; set; }
        // public string map_image { get; set; }
        // public int total_launch_count { get; set; }
        // public int orbital_launch_attempt_count { get; set; }
        // public string fastest_turnaround { get; set; }
        // [JsonProperty("location")]
        // public SpaceDevsLocation location { get; set; }
    }
}