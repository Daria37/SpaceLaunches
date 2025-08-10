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
        Task<List<Rocket>> GetRocketAsync();
        Task<List<Launches>> GetLaunchesAsync();
        Task<List<Agency>> GetAgencyAsync();
        Task<bool> SaveToDatabaseAsync();
    }
}