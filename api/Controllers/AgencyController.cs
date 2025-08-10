using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Agency;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace api.Controllers
{
    [Route("api/agency")]
    [ApiController]
    public class AgencyController : ControllerBase
    {
        private readonly IAgencyRepository _agencyRepo;
        private readonly ISpaceDevsService _spaceDevsService;
        public AgencyController(IAgencyRepository agencyRepo, ISpaceDevsService spaceDevsService)
        {
            _agencyRepo = agencyRepo;
            _spaceDevsService = spaceDevsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var agency = await _agencyRepo.GetAllAsync();
            var agencyDto = agency.Select(s => s.ToAgencyDto());

            return Ok(agencyDto);
        }

        [HttpGet("api")]
        public async Task<ActionResult<SpaceDevsAgency>> GetRocket(CancellationToken ct)
        {
            try
            {
                var agency = await _spaceDevsService.GetAgencyAsync();

                return Ok(agency);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}