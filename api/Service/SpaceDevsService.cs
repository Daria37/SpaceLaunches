using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using api.Interfaces;
using api.Models;
using api.Dtos.Launches;
using System.Text.Json;
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
                var response = await _httpClient.GetAsync("https://ll.thespacedevs.com/2.2.0/launch/?format=json&limit=100");

                var content = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<SpaceDevsResponce>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return apiResponse;
            }
            catch (Exception e)
            {
                return new SpaceDevsResponce { Results = new List<LaunchDto>() };
            }
        }

        public async Task<SpaceDevsResponce> GetData2020Async()
        {
            try
            {
                var response = await _httpClient.GetAsync(
                    "https://ll.thespacedevs.com/2.2.0/launch/?window_start__gte=2020-01-01&format=json&limit=100");
                
                var content = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonSerializer.Deserialize<SpaceDevsResponce>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                return apiResponse;
            }
            catch (Exception e)
            {
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
                ID = launch.Id ?? "0",
                Name = launch.LaunchServiceProvider?.Name ?? "None",
                Image = launch.Image ?? "https://thespacedevs-prod.nyc3.digitaloceanspaces.com/media/images/sputnik_8k74ps_image_20210830185541.jpg",
                Status = launch.Status?.abbrev ?? "None",
                CreatedOnUTC = launch.WindowStart,
                CountryCode = launch.Pad?.Location?.CountryCode ?? "None",
                MapImage = launch.Pad?.Location?.MapImage ?? "None",
                Mission = launch.Mission?.Description ?? "None",
                RocketName = launch.Rocket?.Configuration.Name ?? "None"
            })
            .DistinctBy(launch => launch.ID)
            .ToList();
        }

        public async Task<List<Launches>> GetLaunchesAfter2020Async()
        {
            try
            {
                var response = await GetData2020Async();

                if (response == null)
                {
                    return new List<Launches>();
                }

                if (response.Results == null)
                {
                    return new List<Launches>();
                }

                var launches = new List<Launches>();
                foreach (var launch in response.Results)
                {
                    if (launch == null)
                    {
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
                            RocketName = launch.Rocket?.Configuration.Name ?? "None"
                        };

                        launches.Add(launchEntity);
                    }
                    catch (Exception ex)
                    {
                        return new List<Launches>();
                    }
                }

                return launches.DistinctBy(l => l.ID).ToList();
            }
            catch (Exception ex)
            {
                return new List<Launches>();
            }
        }

        public async Task<List<Launches>> GetLaunchesSpaceX()
        {
            try
            {
                var response = await GetData2020Async();

                if (response == null)
                {
                    return new List<Launches>();
                }

                if (response.Results == null)
                {
                    return new List<Launches>();
                }

                var launches = new List<Launches>();
                foreach (var launch in response.Results)
                {
                    if (launch == null)
                    {
                        continue;
                    }

                    var agencyName = launch.LaunchServiceProvider?.Name;
                    if (agencyName == null || !agencyName.Contains("SpaceX", StringComparison.OrdinalIgnoreCase))
                    {
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
                            RocketName = launch.Rocket?.Configuration.Name ?? "None"
                        };

                        launches.Add(launchEntity);
                    }
                    catch (Exception ex)
                    {
                        return new List<Launches>();
                    }
                }

                return launches.DistinctBy(l => l.ID).ToList();
            }
            catch (Exception ex)
            {
                return new List<Launches>();
            }
        }

        public async Task<Launches> GetLaunchByIdAsync(string id)
        {
            try
            {
                var allLaunches = await GetLaunchesAsync();
                var launch = allLaunches.FirstOrDefault(l => l.ID == id);
                
                if (launch != null)
                {
                    return launch;
                }
                
                var launches2020 = await GetLaunchesAfter2020Async();
                launch = launches2020.FirstOrDefault(l => l.ID == id);
                
                if (launch != null)
                {
                    return launch;
                }
                
                var spacexLaunches = await GetLaunchesSpaceX();
                launch = spacexLaunches.FirstOrDefault(l => l.ID == id);
                
                if (launch != null)
                {
                    return launch;
                }
                
                return null;
            }
            catch (Exception ex)
            {
                return null;
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
                var rockets = await GetRocketAsync();
                var agencies = await GetAgencyAsync();
                var launches = await GetLaunchesAsync();
                var launchesSpaceX = await GetLaunchesSpaceX();
                var launches2020 = await GetLaunchesAfter2020Async();
                var rocket2020 = await GetRocketAfter2020Async();
                var agency2020 = await GetAgencyAfter2020Async();

                var allRockets = rockets.Concat(rocket2020)
                    .GroupBy(r => r.ID)
                    .Select(g => g.First())
                    .ToList();

                var allAgencies = agencies.Concat(agency2020)
                    .GroupBy(a => a.ID)
                    .Select(g => g.First())
                    .ToList();

                var allLaunches = launches.Concat(launchesSpaceX).Concat(launches2020)
                    .GroupBy(l => l.ID)
                    .Select(g => g.First())
                    .ToList();

                _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

                foreach (var rocket in allRockets)
                {
                    var existingRocket = await _context.Rocket
                        .AsNoTracking()
                        .FirstOrDefaultAsync(r => r.ID == rocket.ID);
                        
                    if (existingRocket == null)
                    {
                        _context.Rocket.Add(rocket);
                    }
                }

                foreach (var agency in allAgencies)
                {
                    var existingAgency = await _context.Agency
                        .AsNoTracking()
                        .FirstOrDefaultAsync(a => a.ID == agency.ID);
                        
                    if (existingAgency == null)
                    {
                        _context.Agency.Add(agency);
                    }
                }

                foreach (var launch in allLaunches)
                {
                    var existingLaunch = await _context.Launches
                        .AsNoTracking()
                        .FirstOrDefaultAsync(l => l.ID == launch.ID);
                        
                    if (existingLaunch == null)
                    {
                        _context.Launches.Add(new Launches
                        {
                            ID = launch.ID,
                            Name = launch.Name ?? "None",
                            Image = launch.Image,
                            Status = launch.Status,
                            CreatedOnUTC = launch.CreatedOnUTC,
                            CountryCode = launch.CountryCode ?? "None",
                            MapImage = launch.MapImage,
                            Mission = launch.Mission,
                            RocketName = launch.RocketName
                        });
                    }
                }

                _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;

                var saved = await _context.SaveChangesAsync();
                
                return saved > 0;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}