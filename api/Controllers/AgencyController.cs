using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using api.Dtos.Agency;
using api.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace api.Controllers
{
    [Route("api/agency")]
    [ApiController]
    public class AgencyController : ControllerBase
    {
        private readonly ISpaceDevsService _spaceDevsService;
        public AgencyController(ISpaceDevsService spaceDevsService)
        {
            _spaceDevsService = spaceDevsService;
        }

        [HttpGet("data")]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult<SpaceDevsAgency>> GetRocket(CancellationToken ct)
        {
            try
            {
                var agency = await _spaceDevsService.GetAgencyAfter2020Async();

                return Ok(agency);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}