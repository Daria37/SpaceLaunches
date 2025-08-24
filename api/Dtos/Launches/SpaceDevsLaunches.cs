using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using api.Dtos.Agency;
using api.Dtos.Rocket;
using Newtonsoft.Json;

namespace api.Dtos.Launches
{
    public class SpaceDevsLaunches
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("image")]
        public string Image { get; set; }
        [JsonPropertyName("status")]
        public Status Status { get; set; }
        
        [JsonPropertyName("window_start")]
        public DateTime WindowStart { get; set; }
        
        [JsonPropertyName("launch_service_provider")]
        public LaunchServiceProviderDto LaunchServiceProvider { get; set; }
        
        [JsonPropertyName("rocket")]
        public RocketDto Rocket { get; set; }

        [JsonPropertyName("mission")]
        public Mission Mission { get; set; }
        
        [JsonPropertyName("pad")]
        public PadDto Pad { get; set; }
    }
}