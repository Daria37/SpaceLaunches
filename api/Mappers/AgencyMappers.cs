using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Agency;
using api.Models;

namespace api.Mappers
{
    public static class AgencyMappers
    {
        public static SpaceDevsAgency ToAgencyDto(this Agency agencyModel)
        {
            return new SpaceDevsAgency
            {
                id = agencyModel.ID,
                Name = agencyModel.Name,
                Type = agencyModel.Type,
                CountryCode = agencyModel.CountryCode
            };
        }
    }
}