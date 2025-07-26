using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Components.RouteAttribute;

namespace api.Controllers
{
    [Route("api/launches")]
    [ApiController]
    public class LaunchesController : ControllerBase
    {
        private readonly ILaunchesRepository _launchesRepo;
        public LaunchesController(ILaunchesRepository launchesRepo)
        {
            _launchesRepo = launchesRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var launches = await _launchesRepo.GetAllAsync();
            var launchesDto = launches.Select(s => s.ToLaunchesDto());

            return Ok(launchesDto);
        }
    }
}