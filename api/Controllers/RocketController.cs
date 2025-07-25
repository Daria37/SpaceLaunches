using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Mappers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace api.Controllers
{
    [Route("api/rocket")]
    [ApiController]
    public class RocketController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        public RocketController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var rocket = _context.Rocket.ToList()
            .Select(s => s.ToRocketDto());

            return Ok(rocket);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            var rocket = _context.Rocket.ToList()
            .Select(s => s.ToRocketDto());;

            if (rocket == null)
            {
                return NotFound();
            }

            return Ok(rocket);
        }
    }
}