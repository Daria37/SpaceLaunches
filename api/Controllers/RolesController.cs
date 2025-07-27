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
    [Route("api/roles")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRolesRepository _rolesRepo;
        public RolesController(IRolesRepository rolesRepo)
        {
            _rolesRepo = rolesRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _rolesRepo.GetAllAsync();
            var rolesDto = roles.Select(s => s.ToRolesDto());

            return Ok(rolesDto);
        }
    }
}