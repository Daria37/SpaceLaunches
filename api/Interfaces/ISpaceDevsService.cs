using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Launches;

namespace api.Interfaces
{
    public interface ISpaceDevsService
    {
        Task<SpaceDevsLaunches> GetLaunchesAsync(CancellationToken cancellationToken);
    }
}