using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Rocket;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace api.Controllers
{
    [Route("api/rocket")]
    [ApiController]
    public class RocketController : ControllerBase
    {
        private readonly IRocketRepository _rocketRepo;
        private readonly ISpaceDevsService _spaceDevsService;
        public RocketController(IRocketRepository rocketRepo, ISpaceDevsService spaceDevsService)
        {
            _rocketRepo = rocketRepo;
            _spaceDevsService = spaceDevsService;
        }

        [HttpGet("data")]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult<SpaceDevsRocket>> GetRocket(CancellationToken ct)
        {
            try
            {
                var rocket = await _spaceDevsService.GetRocketAfter2020Async();

                return Ok(rocket);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var rocket = await _rocketRepo.GetAllAsync();
            var rocketDto = rocket.Select(s => s.ToRocketDto());

            return Ok(rocket);
        }
    }
}