using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace api.Dtos.Agency
{
    public class SpaceDevsAgency
    {
        // [JsonPropertyName("response_mode")]
        // public string ResponseMode { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("abbrev")]
        public string Abbrev { get; set; }

        [JsonPropertyName("type")]
        public Type Type { get; set; }

        // [JsonPropertyName("featured")]
        // public bool Featured { get; set; }

        // [JsonPropertyName("country")]
        // public List<Country> Country { get; set; }

        // [JsonPropertyName("description")]
        // public string Description { get; set; }

        // [JsonPropertyName("administrator")]
        // public string Administrator { get; set; }

        // [JsonPropertyName("founding_year")]
        // public int FoundingYear { get; set; }

        [JsonPropertyName("launchers")]
        public string Launchers { get; set; }
    }
}