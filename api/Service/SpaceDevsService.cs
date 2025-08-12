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
        // private readonly RedisCacheService _redisCache;


        public SpaceDevsService(ApplicationDBContext context, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _context = context;
            // _redisCache = redisCache;
            // , RedisCacheService redisCache
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
                            RocketID = launchDto.RocketID ?? 0,
                            AgencyID = launchDto.AgencyID ?? 0,
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

// using System;
// using System.Collections.Generic;
// using System.Data;
// using System.Linq;
// using System.Net;
// using System.Threading.Tasks;
// using api.Interfaces;
// using api.Models;
// using api.Mappers;
// using Microsoft.Identity.Client;
// using api.Dtos.Launches;
// using System.Net.Http;
// using System.Text.Json;
// using Newtonsoft.Json;
// using AutoMapper;
// using System.Text.Json.Serialization;
// using api.Data;
// using Microsoft.EntityFrameworkCore;

// public class SpaceDevsService : ISpaceDevsService
// {
//     private readonly HttpClient _httpClient;
//     private readonly IConfiguration _configuration;
//     private readonly ApplicationDBContext _dbContext;
//     private readonly ILogger<SpaceDevsService> _logger;

//     public SpaceDevsService(
//         HttpClient httpClient,
//         IConfiguration configuration,
//         ApplicationDBContext dbContext,
//         ILogger<SpaceDevsService> logger)
//     {
//         _httpClient = httpClient;
//         _configuration = configuration;
//         _dbContext = dbContext;
//         _logger = logger;
//     }

//     public async Task<SpaceDevsResponce> GetAllDataAsync()
//     {
//          try
//             {
//                 var response = await _httpClient.GetAsync("https://ll.thespacedevs.com/2.2.0/launch/?format=json&limit=10");

//                 var content = await response.Content.ReadAsStringAsync();
//                 var apiResponse = System.Text.Json.JsonSerializer.Deserialize<SpaceDevsResponce>(content, new JsonSerializerOptions
//                 {
//                     PropertyNameCaseInsensitive = true
//                 });
//                 return apiResponse;
//             }
//             catch (Exception e)
//             {
//                 Console.Write(e);
//                 return new SpaceDevsResponce { Results = new List<LaunchDto>() };
//             }
//         }

//     public async Task<bool> SaveToDatabaseAsync(bool forceUpdate = false)
//     {
//         try
//         {
//             if (forceUpdate)
//             {
//                 // Очистка только если явно запрошено
//                 _dbContext.Launches.RemoveRange(_dbContext.Launches);
//                 _dbContext.Rocket.RemoveRange(_dbContext.Rocket);
//                 _dbContext.Agency.RemoveRange(_dbContext.Agency);
//                 await _dbContext.SaveChangesAsync();
//             }

//             var response = await GetAllDataAsync();
            
//             // Словари для отслеживания существующих записей
//             var existingRockets = await _dbContext.Rocket.ToDictionaryAsync(r => r.ID);
//             var existingAgencies = await _dbContext.Agency.ToDictionaryAsync(a => a.ID);
//             var existingLaunches = await _dbContext.Launches.ToDictionaryAsync(l => l.ID);

//             foreach (var launchDto in response.Results)
//             {
//                 try 
//                 {
//                     // Обработка Rocket
//                     if (launchDto.Rocket != null)
//                     {
//                         if (!existingRockets.TryGetValue(launchDto.Rocket.ID, out var rocket))
//                         {
//                             rocket = new Rocket
//                             {
//                                 ID = launchDto.Rocket.ID,
//                                 Name = launchDto.Rocket.Configuration?.Name ?? "None",
//                                 AgencyID = launchDto.LaunchServiceProvider?.Id ?? 0
//                             };
//                             _dbContext.Rocket.Add(rocket);
//                             existingRockets.Add(rocket.ID, rocket);
//                         }
//                     }

//                     // Обработка Agency
//                     if (launchDto.LaunchServiceProvider != null)
//                     {
//                         if (!existingAgencies.TryGetValue(launchDto.LaunchServiceProvider.Id, out var agency))
//                         {
//                             agency = new Agency
//                             {
//                                 ID = launchDto.LaunchServiceProvider.Id,
//                                 Name = launchDto.LaunchServiceProvider.Name,
//                                 Type = launchDto.LaunchServiceProvider.Type,
//                                 CountryCode = launchDto.LaunchServiceProvider.Pad.Location.CountryCode
//                             };
//                             _dbContext.Agency.Add(agency);
//                             existingAgencies.Add(agency.ID, agency);
//                         }
//                     }

//                     // Обработка Launch
//                     if (!existingLaunches.TryGetValue(launchDto.Id, out var launch))
//                     {
//                         launch = new Launches
//                         {
//                             ID = launchDto.Id,
//                             Name = launchDto.Name,
//                             CreatedOnUTC = launchDto.WindowStart,
//                             RocketID = launchDto.Rocket?.ID,
//                             AgencyID = launchDto.LaunchServiceProvider?.Id,
//                             CountryCode = launchDto.LaunchServiceProvider.Pad.Location.CountryCode
//                         };
//                         _dbContext.Launches.Add(launch);
//                     }
//                     else
//                     {
//                         // Обновление существующего запуска
//                         launch.Name = launchDto.Name;
//                         launch.CreatedOnUTC = launchDto.WindowStart;
//                         launch.RocketID = launchDto.Rocket?.ID;
//                         launch.AgencyID = launchDto.LaunchServiceProvider?.Id;
//                         launch.CountryCode = launchDto.LaunchServiceProvider.Pad.Location.CountryCode;
//                     }
//                 }
//                 catch (Exception ex)
//                 {
//                     Console.Write($"Error processing launch {launchDto.Id}");
//                 }
//             }

//             await _dbContext.SaveChangesAsync();
//             return true;
//         }
//         catch (Exception ex)
//         {
//             Console.Write(ex);
//             return false;
//         }
//     }
// }