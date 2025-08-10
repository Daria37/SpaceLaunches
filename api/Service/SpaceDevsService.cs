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
using Microsoft.EntityFrameworkCore;

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
            catch (Exception e)
            {
                Console.Write(e);
                return new SpaceDevsResponce { Results = new List<LaunchDto>() };
            }
        }

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

        public async Task<bool> SaveToDatabaseAsync()
        {
            try
            {
                // Получаем данные из API
                var response = await GetAllDataAsync();

                // Сохраняем ракеты
                var rockets = await GetRocketAsync();

                foreach (var rocketDto in rockets)
                {
                    if (!await _context.Rocket.AnyAsync(r => r.ID == rocketDto.ID))
                    {
                        _context.Rocket.Add(new Rocket
                        {
                            ID = rocketDto.ID,
                            Name = rocketDto.Name ?? "None",
                            AgencyID = rocketDto.AgencyID
                        });
                    }
                }

                // Сохраняем агентства
                var agencies = await GetAgencyAsync();

                foreach (var agencyDto in agencies)
                {
                    if (!await _context.Agency.AnyAsync(a => a.ID == agencyDto.ID))
                    {
                        _context.Agency.Add(new Agency
                        {
                            ID = agencyDto.ID,
                            Name = agencyDto.Name,
                            Type = agencyDto.Type,
                            CountryCode = agencyDto.CountryCode
                        });
                    }
                }

                var launches = await GetLaunchesAsync();

                // Сохраняем запуски
                foreach (var launchDto in launches)
                {
                    if (!await _context.Launches.AnyAsync(l => l.ID == launchDto.ID))
                    {
                        _context.Launches.Add(new Launches
                        {
                            ID = launchDto.ID,
                            Name = launchDto.Name,
                            CreatedOnUTC = launchDto.CreatedOnUTC,
                            RocketID = launchDto.Rocket?.ID,
                            AgencyID = launchDto.Agency?.ID,
                            CountryCode = launchDto.CountryCode
                        });
                    }
                }

                var saved = await _context.SaveChangesAsync();
                Console.Write($"Saved {saved} entities to database");
                return saved > 0;
            }
            catch (Exception ex)
            {
                Console.Write(ex);
                return false;
            }
        }
    }
}