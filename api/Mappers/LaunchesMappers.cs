using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Launches;
using api.Models;

namespace api.Mappers
{
    public static class LaunchesMappers
    {
        public static LaunchesDto ToLaunchesDto(this Launches launchesModel)
        {
            return new LaunchesDto
            {
                ID = launchesModel.ID,
                Name = launchesModel.Name,
                CreatedOnUTC = (DateTime)launchesModel.CreatedOnUTC,
                RocketName = launchesModel.RocketName,
                CountryCode = launchesModel.CountryCode
            };
        }
    }
}