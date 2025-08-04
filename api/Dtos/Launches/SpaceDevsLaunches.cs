using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Agency;
using api.Dtos.Rocket;
using Newtonsoft.Json;

namespace api.Dtos.Launches
{
    public class SpaceDevsLaunches
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        // [JsonProperty("window_end")]
        // public DateTime WindowEnd { get; set; }

        [JsonProperty("window_start")]
        public DateTime WindowStart { get; set; }

        [JsonProperty("launch_service_provider")]
        public SpaceDevsAgency Agencies { get; set; }

        [JsonProperty("rocket")]
        public SpaceDevsRocket Rocket { get; set; }
        [JsonProperty("pad")]
        public SpaceDevsPad pad { get; set; }
    }
}