using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Launches;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace api.Controllers
{
    [Route("api/launches")]
    [ApiController]
    public class LaunchesController : ControllerBase
    {
        private readonly ILaunchesRepository _launchesRepo;
        private readonly ISpaceDevsService _spaceDevsService;
        private readonly ILogger _logger;

        public LaunchesController(ILaunchesRepository launchesRepo, ISpaceDevsService spaceDevsService, ILogger<LaunchesController> logger)
        {
            _launchesRepo = launchesRepo;
            _spaceDevsService = spaceDevsService;
            _logger = logger;
        }

        [HttpGet]
        // public async Task<IActionResult> GetAll([FromQuery] QueryObject query)
        // {
        //     var launches = await _launchesRepo.GetAllAsync(query);
        //     var launchesDto = launches.Select(s => s.ToLaunchesDto());

        //     return Ok(launchesDto);
        // }
        public async Task<ActionResult<SpaceDevsLaunches>> GetLaunches(CancellationToken ct)
        {
            try
            {
                var launches = await _spaceDevsService.GetLaunchesAsync(ct);

                return Ok(launches);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}