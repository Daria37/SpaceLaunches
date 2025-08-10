using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Rocket;
using api.Interfaces;
using api.Mappers;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IRocketRepository _rocketRepo;
        private readonly ISpaceDevsService _spaceDevsService;
        public RocketController(ApplicationDBContext context, IRocketRepository rocketRepo, ISpaceDevsService spaceDevsService)
        {
            _rocketRepo = rocketRepo;
            _context = context;
            _spaceDevsService = spaceDevsService;
        }

        [HttpGet("api")]
        public async Task<ActionResult<SpaceDevsRocket>> GetRocket(CancellationToken ct)
        {
            try
            {
                var rocket = await _spaceDevsService.GetRocketAsync();

                return Ok(rocket);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var rocket = await _rocketRepo.GetAllAsync();
            var rocketDto = rocket.Select(s => s.ToRocketDto());

            return Ok(rocket);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var rocket = await _rocketRepo.GetByIdAsync(id);

            if (rocket == null)
            {
                return NotFound();
            }

            return Ok(rocket);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRocketRequestDto rocketDto)
        {
            var rocketModel = rocketDto.ToRocketFromCreateDTO();
            await _rocketRepo.CreateAsync(rocketModel);
            return CreatedAtAction(nameof(GetById), new { id = rocketModel.ID }, rocketModel.ToRocketDto());
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateRocketRequestDto updateDto)
        {
            var rocketModel = await _rocketRepo.UpdateAsync(id, updateDto);
            if (rocketModel == null)
            {
                return NotFound();
            }

            return Ok(rocketModel.ToRocketDto());
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var rocketModel = await _rocketRepo.DeleteAsync(id);
            if (rocketModel == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}