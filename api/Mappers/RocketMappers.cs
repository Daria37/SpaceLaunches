using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Rocket;
using api.Models;

namespace api.Mappers
{
    public static class RocketMappers
    {
        public static RocketDto ToRocketDto(this Rocket rocketModel)
        {
            return new RocketDto
            {
                ID = rocketModel.ID
            };
        }
    }
}