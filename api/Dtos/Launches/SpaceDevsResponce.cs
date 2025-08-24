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
    public class SpaceDevsResponce
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }
        
        [JsonPropertyName("next")]
        public string Next { get; set; }
        
        [JsonPropertyName("previous")]
        public string Previous { get; set; }
        
        [JsonPropertyName("results")]
        public List<LaunchDto> Results { get; set; }
    }

    public class LaunchDto
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

    public class RocketDto
    {
        [JsonPropertyName("id")]
        public int ID { get; set; }
        [JsonPropertyName("configuration")]
        public RocketConfigurationDto Configuration { get; set; }
        [JsonPropertyName("launch_service_provider")]
        public LaunchServiceProviderDto LaunchServiceProvider { get; set; }
    }
    public class Status
    {
        [JsonPropertyName("abbrev")]
        public string abbrev { get; set; }
    }

    public class Mission
    {
        [JsonPropertyName("description")]
        public string Description { get; set; }
    }

    public class LaunchServiceProviderDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("pad")]
        public PadDto Pad { get; set; }
    }

    public class RocketConfigurationDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }

    public class PadDto
    {
        [JsonPropertyName("location")]
        public LocationDto Location { get; set; }
    }

    public class LocationDto
    {
        [JsonPropertyName("country_code")]
        public string CountryCode { get; set; }

        [JsonPropertyName("map_image")]
        public string MapImage { get; set; }
    }
}