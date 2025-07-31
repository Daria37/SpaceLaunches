using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using api.Mappers;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.Identity.Client;
using api.Dtos.Launches;
using System.Net.Http;
using static api.Dtos.Launches.SpaceDevsLaunches;
using api.Dtos;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace api.Service
{
    public class SpaceDevsService(IHttpClientFactory httpClientFactory) : ISpaceDevsService
    //  : ISpaceDevsService
    {
        // public async Task<SpaceDevsLaunches?> GetLaunchesAsync(CancellationToken cancellationToken = default)
        public async Task<List<SpaceDevsLaunches?>> GetLaunchesAsync(CancellationToken cancellationToken = default)
        {
            // var client = httpClientFactory.CreateClient("Client");
            // string result = await client.GetStringAsync("launch/?format=json&limit=10&offset=0");
            // // return JsonConvert.DeserializeObject<SpaceDevsLaunches>(result);
            // var jObject = JObject.Parse(result);
            // var firstLaunch = jObject["results"]?[0]?.ToString();
            // return JsonConvert.DeserializeObject<SpaceDevsLaunches>(firstLaunch);
            var client = httpClientFactory.CreateClient("Client");
            string result = await client.GetStringAsync("launch/?format=json&limit=10&offset=0");
            var response = JsonConvert.DeserializeObject<SpaceDevsResponce>(result);
            return response?.Results ?? new List<SpaceDevsLaunches>();
        }
    }
}