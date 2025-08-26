using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Launches;
using api.Models;

namespace api.Interfaces
{
    public interface ISpaceDevsService
    {
        Task<SpaceDevsResponce> GetAllDataAsync();
        Task<SpaceDevsResponce> GetData2020Async();
        Task<List<Rocket>> GetRocketAsync();
        Task<List<Launches>> GetLaunchesAsync();
        Task<List<Launches>> GetLaunchesSpaceX();
        Task<Launches> GetLaunchByIdAsync(string id);
        Task<List<Launches>> GetLaunchesAfter2020Async();
        Task<List<Agency>> GetAgencyAfter2020Async();
        Task<List<Rocket>> GetRocketAfter2020Async();
        Task<List<Agency>> GetAgencyAsync();
        Task<bool> SaveToDatabaseAsync();
    }
}