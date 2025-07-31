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

        public static Launches ToLaunchesFromSpaceDevs(this SpaceDevsLaunches spacedevsLaunches, SpaceDevsAgency spaceDevsAgency, SpaceDevsRocket spaceDevsRocket, SpaceDevsPad spaceDevsPad)
        {
            return new Launches
            {
                ID = int.Parse(spacedevsLaunches.Id),
                Name = spaceDevsRocket.Configuration.name,
                CreatedOnUTC = spacedevsLaunches.WindowStart,
                RocketID = spaceDevsRocket.Id,
                AgencyID = spaceDevsAgency.id
                // CountryCode = spaceDevsPad.location.country.alpha_2_code
            };
        }
    }
}