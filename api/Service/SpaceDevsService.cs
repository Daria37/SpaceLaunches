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
using AutoMapper;
using System.Text.Json.Serialization;
using api.Data;

namespace api.Service
{
    public class SpaceDevsService : ISpaceDevsService
    {
        private readonly HttpClient _httpClient;
        private readonly ApplicationDBContext _context;

        public SpaceDevsService(ApplicationDBContext context, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _context = context;
        }

        public async Task<SpaceDevsResponce> GetAllDataAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://ll.thespacedevs.com/2.2.0/launch/?format=json&limit=10");

                var content = await response.Content.ReadAsStringAsync();
                var apiResponse = System.Text.Json.JsonSerializer.Deserialize<SpaceDevsResponce>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return apiResponse;
            }
            catch (Exception ex)
            {
                return new SpaceDevsResponce { Results = new List<LaunchDto>() };
            }
        }


        // public async Task<List<Launches>> GetLaunchesAsync()
        // {
        //     try
        //     {
        //         var response = await _httpClient.GetAsync("https://ll.thespacedevs.com/2.2.0/launch/?format=json&limit=10");
        //         response.EnsureSuccessStatusCode();

        //         var json = await response.Content.ReadAsStringAsync();
        //         var apiResponse = System.Text.Json.JsonSerializer.Deserialize<SpaceDevsResponce>(json, new JsonSerializerOptions
        //         {
        //             PropertyNameCaseInsensitive = true
        //         });

        //         if (apiResponse?.Results == null)
        //         {
        //             return new List<Launches>();
        //         }

        //         var result = new List<Launches>();
        //         foreach (var launchDto in apiResponse.Results)
        //         {
        //             if (!_context.Launches.Any(l => l.ID == launchDto.Id))
        //             {
        //                 var newLaunch = new Launches
        //                 {
        //                     ID = launchDto.Id,
        //                     Name = launchDto.Name,
        //                     CreatedOnUTC = launchDto.WindowStart,
        //                     AgencyID = launchDto.LaunchServiceProvider?.Id ?? 0,
        //                     RocketID = launchDto.Rocket?.ID ?? 0,
        //                     CountryCode = launchDto.Pad?.Location.CountryCode ?? "N/A",
        //                 };
        //             }
        //         }

        //         // await _context.Launches.AddRangeAsync(result);
        //         // await _context.SaveChangesAsync();

        //         return result;
        //     }
        //     catch (Exception ex)
        //     {
        //         Console.WriteLine($"Ошибка при получении данных: {ex.Message}");
        //         throw;
        //     }
        // }

        public async Task<List<Rocket>> GetRocketAsync()
        {
            var response = await GetAllDataAsync();
            return response.Results
            .Where(launch => launch.Rocket != null)
            .Select(launch => new Rocket
            {
                ID = launch.Rocket.ID,
                Name = launch.Rocket.Configuration?.Name ?? "None",
                AgencyID = launch.LaunchServiceProvider?.Id ?? 0
            })
            .DistinctBy(rocket => rocket.ID)
            .ToList();
        }

        public async Task<List<Launches>> GetLaunchesAsync()
        {
            var response = await GetAllDataAsync();
            return response.Results
            .Where(launch => launch != null)
            .Select(launch => new Launches
            {
                ID = launch.Id,
                Name = launch.Name ?? "None",
                CreatedOnUTC = launch.WindowStart,
                CountryCode = launch.Pad?.Location.CountryCode ?? "None",
                RocketID = launch.Rocket?.ID ?? 0,
                AgencyID = launch.LaunchServiceProvider?.Id ?? 0
            })
            .DistinctBy(launch => launch.ID)
            .ToList();
        }

        public async Task<List<Agency>> GetAgencyAsync()
        {
            var response = await GetAllDataAsync();
            return response.Results
            .Where(launch => launch.LaunchServiceProvider != null)
            .Select(launch => new Agency
            {
                ID = launch.LaunchServiceProvider.Id,
                Name = launch.LaunchServiceProvider.Name ?? "None",
                Type = launch.LaunchServiceProvider.Type ?? "None",
                CountryCode = launch.Pad?.Location.CountryCode ?? "None"
            })
            .DistinctBy(agency => agency.ID)
            .ToList();
        }


        // public async Task<List<Rocket>> GetRocketAsync()
        // {
        //     try
        //     {
        //         var response = await _httpClient.GetAsync("https://ll.thespacedevs.com/2.2.0/launch/?format=json&limit=10");
        //         response.EnsureSuccessStatusCode();

        //         var json = await response.Content.ReadAsStringAsync();
        //         var apiResponse = System.Text.Json.JsonSerializer.Deserialize<SpaceDevsResponce>(json, new JsonSerializerOptions
        //         {
        //             PropertyNameCaseInsensitive = true
        //         });

        //         if (apiResponse?.Results == null)
        //         {
        //             return new List<Rocket>();
        //         }

        //         var result = new List<Rocket>();
        //         foreach (var rocketDto in apiResponse.Results)
        //         {
        //             if (!_context.Rocket.Any(l => l.ID == rocketDto.Id))
        //             {
        //                 var newRocket = new Rocket
        //                 {
        //                     ID = rocketDto.Id,
        //                     Name = launchDto.Name,
        //                     CreatedOnUTC = launchDto.WindowStart,
        //                     AgencyID = launchDto.LaunchServiceProvider?.Id ?? 0,
        //                     RocketID = launchDto.Rocket?.Id ?? 0,
        //                     CountryCode = launchDto.Pad?.Location.CountryCode ?? "N/A",
        //                 };
        //             }
        //         }

        //         // await _context.Launches.AddRangeAsync(result);
        //             // await _context.SaveChangesAsync();

        //             return result;
        //     }
        //     catch (Exception ex)
        //     {
        //         Console.WriteLine($"Ошибка при получении данных: {ex.Message}");
        //         throw;
        //     }
        // }
    }
}