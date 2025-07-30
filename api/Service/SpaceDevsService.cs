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

namespace api.Service
{
    public class SpaceDevsService(IHttpClientFactory httpClientFactory) : ISpaceDevsService
    {
        public async Task<SpaceDevsLaunches?> GetLaunchesAsync(CancellationToken cancellationToken = default)
        {
            var client = httpClientFactory.CreateClient("Client");
            string a = await client.GetStringAsync("launch/?format=json&limit=10&offset=0");
            return JsonConvert.DeserializeObject<SpaceDevsLaunches>(a);
        }

        // private HttpClient _httpClient;
        // private IConfiguration _config;
        // public SpaceDevsService(HttpClient httpClient, IConfiguration config)
        // {
        //     _httpClient = httpClient;
        //     _config = config;
        // }

        // public async Task<SpaceDevsLaunches> GetLaunchesAsync(CancellationToken ct)
        // {
        //     try
        //     {
        //         var result = await _httpClient.GetAsync($"https://ll.thespacedevs.com/2.2.0/launch/?limit=10&offset=0&ordering=-net");
        //         if (result.IsSuccessStatusCode)
        //         {
        //             var content = await result.Content.ReadAsStringAsync();
        //             var tasks = JsonConvert.DeserializeObject<SpaceDevsLaunches[]>(content);
        //             var launch = tasks[0];
        //             // if (launch != null)
        //             // {
        //             //     return launch.ToLaunchesFromSpaceDevs();
        //             // }
        //             return launch;
        //         }
        //     }
        //     catch (Exception e)
        //     {
        //         Console.WriteLine(e);
        //         return null;
        //     }
        // }
    }
}