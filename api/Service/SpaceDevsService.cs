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
                // 2020-10-06T11:29:34Z
                // https://ll.thespacedevs.com/2.3.0/launches/?agency_launch_attempt_count=&agency_launch_attempt_count__gt=&agency_launch_attempt_count__gte=&agency_launch_attempt_count__lt=&agency_launch_attempt_count__lte=&agency_launch_attempt_count_year=&agency_launch_attempt_count_year__gt=&agency_launch_attempt_count_year__gte=&agency_launch_attempt_count_year__lt=&agency_launch_attempt_count_year__lte=&day=&format=json&id=&include_suborbital=unknown&is_crewed=unknown&last_updated__gte=&last_updated__lte=&launch_designator=&launcher_config__id=&location__ids=&location_launch_attempt_count=&location_launch_attempt_count__gt=&location_launch_attempt_count__gte=&location_launch_attempt_count__lt=&location_launch_attempt_count__lte=&location_launch_attempt_count_year=&location_launch_attempt_count_year__gt=&location_launch_attempt_count_year__gte=&location_launch_attempt_count_year__lt=&location_launch_attempt_count_year__lte=&lsp__id=&lsp__name=&mission__orbit__celestial_body__id=&mission__orbit__name=&mission__orbit__name__icontains=&month=&name=&net__gt=&net__gte=&net__lt=&net__lte=&orbital_launch_attempt_count=&orbital_launch_attempt_count__gt=&orbital_launch_attempt_count__gte=&orbital_launch_attempt_count__lt=&orbital_launch_attempt_count__lte=&orbital_launch_attempt_count_year=&orbital_launch_attempt_count_year__gt=&orbital_launch_attempt_count_year__gte=&orbital_launch_attempt_count_year__lt=&orbital_launch_attempt_count_year__lte=&pad=&pad__location=&pad__location__celestial_body__id=&pad_launch_attempt_count=&pad_launch_attempt_count__gt=&pad_launch_attempt_count__gte=&pad_launch_attempt_count__lt=&pad_launch_attempt_count__lte=&pad_launch_attempt_count_year=&pad_launch_attempt_count_year__gt=&pad_launch_attempt_count_year__gte=&pad_launch_attempt_count_year__lt=&pad_launch_attempt_count_year__lte=&related_lsp__id=&related_lsp__name=&rocket__configuration__full_name=&rocket__configuration__full_name__icontains=&rocket__configuration__id=&rocket__configuration__manufacturer__name=&rocket__configuration__manufacturer__name__icontains=&rocket__configuration__name=&rocket__spacecraftflight__spacecraft__id=&rocket__spacecraftflight__spacecraft__name=&rocket__spacecraftflight__spacecraft__name__icontains=&serial_number=&slug=&spacecraft_config__ids=&status=&status__ids=&video_url=&window_end__gt=&window_end__gte=&window_end__lt=&window_end__lte=&window_start__gt=&window_start__gte=2020-10-06T11%3A29%3A34Z&window_start__lt=&window_start__lte=&year=
                var response = await _httpClient.GetAsync("https://ll.thespacedevs.com/2.2.0/launch/?format=json&limit=50");

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

        public async Task<SpaceDevsResponce> GetData2020Async()
        {
            try
            {
                // Фильтр по дате старта (после 1 января 2020)
                var response = await _httpClient.GetAsync(
                    "https://ll.thespacedevs.com/2.2.0/launch/?window_start__gte=2020-01-01&format=json&limit=10");
                
                var content = await response.Content.ReadAsStringAsync();
                var apiResponse = System.Text.Json.JsonSerializer.Deserialize<SpaceDevsResponce>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return apiResponse;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
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

        // public async Task<List<Launches>> GetLaunchesAsync()
        // {
        //     var response = await GetAllDataAsync();
        //     return response.Results
        //     .Where(launch => launch != null)
        //     .Select(launch => new Launches
        //     {
        //         ID = launch.Id,
        //         Name = launch.LaunchServiceProvider.Name ?? "None",
        //         Image = launch.Image,
        //         Status = launch.Status.abbrev,
        //         CreatedOnUTC = launch.WindowStart,
        //         CountryCode = launch.Pad?.Location.CountryCode ?? "None",
        //         MapImage = launch.Pad.Location.MapImage,
        //         Mission = launch.Mission.Description,
        //         RocketID = launch.Rocket?.ID,
        //         AgencyID = launch.LaunchServiceProvider?.Id
        //     })
        //     .DistinctBy(launch => launch.ID)
        //     .ToList();
        // }
        
        public async Task<List<Launches>> GetLaunchesAsync()
        {
            var response = await GetAllDataAsync();
            return response.Results
            .Where(launch => launch != null)
            .Select(launch => new Launches
            {
                ID = launch.Id ?? "0",
                Name = launch.LaunchServiceProvider?.Name ?? "None",
                Image = launch.Image ?? "https://thespacedevs-prod.nyc3.digitaloceanspaces.com/media/images/sputnik_8k74ps_image_20210830185541.jpg",
                Status = launch.Status?.abbrev ?? "None",
                CreatedOnUTC = launch.WindowStart,
                CountryCode = launch.Pad?.Location?.CountryCode ?? "None",
                MapImage = launch.Pad?.Location?.MapImage ?? "None",
                Mission = launch.Mission?.Description ?? "None",
                RocketID = launch.Rocket?.ID ?? 0,
                AgencyID = launch.LaunchServiceProvider?.Id ?? 0
            })
            .DistinctBy(launch => launch.ID)
            .ToList();
        }

        // public async Task<List<Launches>> GetLaunchesAfter2020Async()
        // {
        //     var response = await GetData2020Async();
        //     return response.Results
        //     .Where(launch => launch != null)
        //     .Select(launch => new Launches
        //     {
        //         ID = launch.Id ?? "0",
        //         Name = launch.LaunchServiceProvider.Name ?? "None",
        //         Image = launch.Image ?? "None",
        //         Status = launch.Status.abbrev ?? "None",
        //         CreatedOnUTC = launch.WindowStart,
        //         CountryCode = launch.Pad?.Location.CountryCode ?? "None",
        //         MapImage = launch.Pad.Location.MapImage ?? "None",
        //         Mission = launch.Mission.Description ?? "None",
        //         RocketID = launch.Rocket?.ID ?? 0,
        //         AgencyID = launch.LaunchServiceProvider?.Id ?? 0
        //     })
        //     .DistinctBy(launch => launch.ID)
        //     .ToList();
        // }

        public async Task<List<Launches>> GetLaunchesAfter2020Async()
        {
            try
            {
                Console.WriteLine("Getting launches after 2020...");
                var response = await GetData2020Async();

                if (response == null)
                {
                    Console.WriteLine("Response is null");
                    return new List<Launches>();
                }

                if (response.Results == null)
                {
                    Console.WriteLine("Response.Results is null");
                    return new List<Launches>();
                }

                Console.WriteLine($"Found {response.Results.Count} launches");

                var launches = new List<Launches>();
                foreach (var launch in response.Results)
                {
                    if (launch == null)
                    {
                        Console.WriteLine("Skipping null launch");
                        continue;
                    }

                    try
                    {
                        var launchEntity = new Launches
                        {
                            ID = launch.Id ?? "0",
                            Name = launch.LaunchServiceProvider?.Name ?? "None",
                            Image = launch.Image ?? "None",
                            Status = launch.Status?.abbrev ?? "None",
                            CreatedOnUTC = launch.WindowStart,
                            CountryCode = launch.Pad?.Location?.CountryCode ?? "None",
                            MapImage = launch.Pad?.Location?.MapImage ?? "None",
                            Mission = launch.Mission?.Description ?? "None",
                            RocketID = launch.Rocket?.ID ?? 0,
                            AgencyID = launch.LaunchServiceProvider?.Id ?? 0
                        };

                        launches.Add(launchEntity);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error processing launch {launch.Id}: {ex.Message}");
                    }
                }

                Console.WriteLine($"Successfully processed {launches.Count} launches");
                return launches.DistinctBy(l => l.ID).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetLaunchesAfter2020Async: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                return new List<Launches>();
            }
        }

        public async Task<List<Rocket>> GetRocketAfter2020Async()
        {
            var response = await GetData2020Async();
            return response.Results
            .Where(rocket => rocket != null)
            .Select(rocket => new Rocket
            {
                ID = rocket.Rocket.ID,
                Name = rocket.Rocket.Configuration?.Name ?? "None",
                AgencyID = rocket.LaunchServiceProvider?.Id ?? 0
            })
            .DistinctBy(rocket => rocket.ID)
            .ToList();
        }

        public async Task<List<Agency>> GetAgencyAfter2020Async()
        {
            var response = await GetData2020Async();
            return response.Results
            .Where(agency => agency != null)
            .Select(agency => new Agency
            {
                ID = agency.LaunchServiceProvider.Id,
                Name = agency.LaunchServiceProvider.Name ?? "None",
                Type = agency.LaunchServiceProvider.Type ?? "None",
                CountryCode = agency.Pad?.Location.CountryCode ?? "None"
            })
            .DistinctBy(agency => agency.ID)
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
                            Name = launchDto.Name ?? "None",
                            Image = launchDto.Image,
                            Status = launchDto.Status,
                            CreatedOnUTC = launchDto.CreatedOnUTC,
                            CountryCode = launchDto.CountryCode ?? "None",
                            MapImage = launchDto.MapImage,
                            Mission = launchDto.Mission,
                            RocketID = launchDto.Rocket?.ID,
                            AgencyID = launchDto.AgencyID ?? 0
                        });
                    }
                }

                var launches2020 = await GetLaunchesAfter2020Async();

                // Сохраняем запуски
                foreach (var launchDto in launches2020)
                {
                    if (!await _context.Launches.AnyAsync(l => l.ID == launchDto.ID))
                    {
                        _context.Launches.Add(new Launches
                        {
                            ID = launchDto.ID,
                            Name = launchDto.Name ?? "None",
                            Image = launchDto.Image,
                            Status = launchDto.Status,
                            CreatedOnUTC = launchDto.CreatedOnUTC,
                            CountryCode = launchDto.CountryCode ?? "None",
                            MapImage = launchDto.MapImage,
                            Mission = launchDto.Mission,
                            RocketID = launchDto.Rocket?.ID,
                            AgencyID = launchDto.AgencyID ?? 0
                        });
                    }
                }

                var rocket2020 = await GetRocketAfter2020Async();

                // Сохраняем запуски
                foreach (var rocketDto in rocket2020)
                {
                    if (!await _context.Rocket.AnyAsync(l => l.ID == rocketDto.ID))
                    {
                        _context.Rocket.Add(new Rocket
                        {
                            ID = rocketDto.ID,
                            Name = rocketDto.Name,
                            AgencyID = rocketDto.AgencyID
                        });
                    }
                }

                var agency2020 = await GetAgencyAfter2020Async();

                // Сохраняем запуски
                foreach (var agencyDto in agency2020)
                {
                    if (!await _context.Agency.AnyAsync(l => l.ID == agencyDto.ID))
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