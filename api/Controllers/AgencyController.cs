using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public AgencyController(IAgencyRepository agencyRepo)
        {
            _agencyRepo = agencyRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var agency = await _agencyRepo.GetAllAsync();
            var agencyDto = agency.Select(s => s.ToAgencyDto());

            return Ok(agencyDto);
        }
    }
}