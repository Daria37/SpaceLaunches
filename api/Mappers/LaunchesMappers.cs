using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Agency;
using api.Dtos.Launches;
using api.Dtos.Rocket;
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
                CreatedOnUTC = launchesModel.CreatedOnUTC,
                RocketID = launchesModel.RocketID,
                AgencyID = launchesModel.AgencyID,
                CountryCode = launchesModel.CountryCode
            };
        }

        public static Launches ToLaunchesFromSpaceDevs(this SpaceDevsLaunches spacedevsLaunches)
        {
            return new Launches
            {
                ID = spacedevsLaunches.Id,
                Name = spacedevsLaunches.Name,
                CreatedOnUTC = spacedevsLaunches.WindowStart,
                RocketID = spacedevsLaunches.Rocket.Id,
                AgencyID = spacedevsLaunches.Agencies.id,
                CountryCode = spacedevsLaunches.pad.country
            };
        }
    }
}