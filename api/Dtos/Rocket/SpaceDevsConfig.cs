using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace api.Dtos.Rocket
{
    public class SpaceDevsConfig
    {
        // [JsonPropertyName("id")]
        // public string response_mode { get; set; }
        // [JsonPropertyName("id")]
        // public int id { get; set; }
        // [JsonPropertyName("id")]
        // public string url { get; set; }
        [JsonPropertyName("name")]
        public string name { get; set; }
        // [JsonPropertyName("id")]
        // public List<Family> families { get; set; }
        // [JsonPropertyName("id")]
        // public string full_name { get; set; }
        // [JsonPropertyName("id")]
        // public string variant { get; set; }
    }
}