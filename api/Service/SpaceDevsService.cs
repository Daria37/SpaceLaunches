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

namespace api.Service
{
    public class SpaceDevsService : ISpaceDevsService
    {
        private readonly HttpClient _httpClient;

        public SpaceDevsService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Launches>> GetLaunchesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync("https://ll.thespacedevs.com/2.2.0/launch/?format=json&limit=10");
                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();
                var apiResponse = System.Text.Json.JsonSerializer.Deserialize<SpaceDevsResponce>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (apiResponse?.Results == null)
                {
                    return new List<Launches>();
                }

                var result = new List<Launches>();
                foreach (var launchDto in apiResponse.Results)
                {
                    result.Add(new Launches
                    {
                        ID = launchDto.Id,
                        Name = launchDto.Name,
                        CreatedOnUTC = launchDto.WindowStart,
                        AgencyID = launchDto.LaunchServiceProvider?.Id ?? 0,
                        RocketID = launchDto.Rocket?.Id ?? 0,
                        CountryCode = launchDto.Pad?.Location.CountryCode ?? "N/A"
                    });
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при получении данных: {ex.Message}");
                throw;
            }
        }
    }
}