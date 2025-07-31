using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace api.Dtos.Rocket
{
    public class SpaceDevsRocket
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("configuration")]
        public SpaceDevsConfig Configuration { get; set; }
    }
}