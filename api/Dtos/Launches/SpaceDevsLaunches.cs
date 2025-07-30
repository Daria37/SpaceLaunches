using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using api.Dtos.Agency;
using api.Dtos.Rocket;

namespace api.Dtos.Launches
{
    public class SpaceDevsLaunches
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("window_end")]
        public DateTime WindowEnd { get; set; }

        [JsonPropertyName("window_start")]
        public DateTime WindowStart { get; set; }

        [JsonPropertyName("alpha_3_code")]
        public string Alpha3Code { get; set; }
        [JsonPropertyName("agencies")]
        public List<SpaceDevsAgency> Agencies { get; set; }

        [JsonPropertyName("rocket")]
        public SpaceDevsRocket Rocket { get; set; }

            // public class Country
            // {
            //     [JsonPropertyName("id")]
            //     public int Id { get; set; }

            //     [JsonPropertyName("name")]
            //     public string Name { get; set; }

            //     [JsonPropertyName("alpha_2_code")]
            //     public string Alpha2Code { get; set; }

            //     [JsonPropertyName("alpha_3_code")]
            //     public string Alpha3Code { get; set; }

            //     [JsonPropertyName("nationality_name")]
            //     public string NationalityName { get; set; }

            //     [JsonPropertyName("nationality_name_composed")]
            //     public string NationalityNameComposed { get; set; }
            // }

            // public class LaunchServiceProvider
            // {
            //     [JsonPropertyName("response_mode")]
            //     public string ResponseMode { get; set; }

            //     [JsonPropertyName("id")]
            //     public int Id { get; set; }

            //     [JsonPropertyName("url")]
            //     public string Url { get; set; }

            //     [JsonPropertyName("name")]
            //     public string Name { get; set; }

            //     [JsonPropertyName("abbrev")]
            //     public string Abbrev { get; set; }

            //     [JsonPropertyName("type")]
            //     public Type Type { get; set; }
            // }

            // public class Location
            // {
            //     [JsonPropertyName("response_mode")]
            //     public string ResponseMode { get; set; }

            //     [JsonPropertyName("id")]
            //     public int Id { get; set; }

            //     [JsonPropertyName("url")]
            //     public string Url { get; set; }

            //     [JsonPropertyName("name")]
            //     public string Name { get; set; }

            //     [JsonPropertyName("active")]
            //     public bool Active { get; set; }

            //     [JsonPropertyName("country")]
            //     public Country Country { get; set; }

            //     [JsonPropertyName("description")]
            //     public string Description { get; set; }

            //     [JsonPropertyName("map_image")]
            //     public string MapImage { get; set; }

            //     [JsonPropertyName("longitude")]
            //     public double Longitude { get; set; }

            //     [JsonPropertyName("latitude")]
            //     public double Latitude { get; set; }

            //     [JsonPropertyName("timezone_name")]
            //     public string TimezoneName { get; set; }

            //     [JsonPropertyName("total_launch_count")]
            //     public int TotalLaunchCount { get; set; }

            //     [JsonPropertyName("total_landing_count")]
            //     public int TotalLandingCount { get; set; }
            // }

            // public class Pad
            // {
            //     [JsonPropertyName("id")]
            //     public int Id { get; set; }

            //     [JsonPropertyName("url")]
            //     public string Url { get; set; }

            //     [JsonPropertyName("active")]
            //     public bool Active { get; set; }

            //     [JsonPropertyName("agencies")]
            //     public List<Agency> Agencies { get; set; }

            //     [JsonPropertyName("name")]
            //     public string Name { get; set; }

            //     [JsonPropertyName("image")]
            //     public object Image { get; set; }

            //     [JsonPropertyName("description")]
            //     public object Description { get; set; }

            //     [JsonPropertyName("info_url")]
            //     public object InfoUrl { get; set; }

            //     [JsonPropertyName("wiki_url")]
            //     public string WikiUrl { get; set; }

            //     [JsonPropertyName("map_url")]
            //     public string MapUrl { get; set; }

            //     [JsonPropertyName("latitude")]
            //     public double Latitude { get; set; }

            //     [JsonPropertyName("longitude")]
            //     public double Longitude { get; set; }

            //     [JsonPropertyName("country")]
            //     public Country Country { get; set; }

            //     [JsonPropertyName("map_image")]
            //     public string MapImage { get; set; }

            //     [JsonPropertyName("total_launch_count")]
            //     public int TotalLaunchCount { get; set; }

            //     [JsonPropertyName("orbital_launch_attempt_count")]
            //     public int OrbitalLaunchAttemptCount { get; set; }

            //     [JsonPropertyName("fastest_turnaround")]
            //     public string FastestTurnaround { get; set; }

            //     [JsonPropertyName("location")]
            //     public Location Location { get; set; }
            // }
    }
}