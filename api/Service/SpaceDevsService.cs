using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using api.Mappers;
using Microsoft.Identity.Client;
using api.Dtos.Launches;
using System.Net.Http;
using System.Text.Json;
using Newtonsoft.Json;

namespace api.Service
{
    public class SpaceDevsService(IHttpClientFactory httpClientFactory) : ISpaceDevsService
    // ISpaceDevsService
    // {
    // public async Task<List<SpaceDevsLaunches?>> GetLaunchesAsync(CancellationToken cancellationToken = default)
    {
            // private readonly HttpClient _httpClient;
            // private readonly IHttpClientFactory _httpClientFactory;
            // private IConfiguration _config;
        // public SpaceDevsService(HttpClient httpClient, IConfiguration config)
        // {
        //     _httpClient = httpClient;
        //     _config = config;
        // }
            // public async Task<List<SpaceDevsLaunches?>> GetLaunchesAsync()
            public async Task<List<SpaceDevsLaunches?>> GetLaunchesAsync(CancellationToken cancellationToken = default)
            {
                var client = httpClientFactory.CreateClient("Client");
                var response = await client.GetStringAsync("launch/?format=json&limit=10&offset=0");
                var result = JsonConvert.DeserializeObject<SpaceDevsResponce>(response);
                return result?.Results ?? new List<SpaceDevsLaunches>();
                // var client = httpClientFactory.CreateClient("Client");
                // string result = await client.GetStringAsync("launch/?format=json&limit=10&offset=0");
                // var response = JsonConvert.DeserializeObject<SpaceDevsResponce>(result);
                // return response?.Results ?? new List<SpaceDevsLaunches>();
            }
    }
}