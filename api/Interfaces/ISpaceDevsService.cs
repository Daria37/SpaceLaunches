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
        Task<List<SpaceDevsLaunches>> GetLaunchesAsync(CancellationToken cancellationToken = default);
        // Task<List<SpaceDevsLaunches>> GetLaunchesAsync();
    }
}