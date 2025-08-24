using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Rocket;
using api.Interfaces;
using api.Mappers;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
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

        // [HttpGet("search")]
        // [Authorize(Roles = "User,Admin")]
        // public async Task<ActionResult<IEnumerable<Rocket>>> SearchRocket(
        //     [FromQuery] string? searchTerm,
        //     // [FromQuery] string? countryCode,
        //     [FromServices] IDistributedCache cache)
        // {
        //     // Формируем ключ кэша на основе всех параметров фильтрации
        //     // var cacheKey = $"rocket_search_{searchTerm}_{countryCode}";
        //     var cacheKey = $"rocket_search_{searchTerm}";

        //     // Проверяем кэш
        //     var cachedData = await cache.GetStringAsync(cacheKey);
        //     if (cachedData != null)
        //     {
        //         return Ok(JsonSerializer.Deserialize<List<Rocket>>(cachedData));
        //     }

        //     // Запрос к БД с учетом фильтров
        //     var query = _context.Rocket.AsQueryable();

        //     if (!string.IsNullOrEmpty(searchTerm))
        //     {
        //         query = query.Where(l => l.Name.Contains(searchTerm));
        //     }

        //     var rockets = await query.ToListAsync();

        //     // Сохраняем в кэш
        //     await cache.SetStringAsync(
        //         cacheKey,
        //         JsonSerializer.Serialize(rockets),
        //         new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) }
        //     );

        //     return Ok(rockets);
        // }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var rocket = await _rocketRepo.GetAllAsync();
            var rocketDto = rocket.Select(s => s.ToRocketDto());

            return Ok(rocket);
        }

        // [HttpGet("{id:int}")]
        // [Authorize(Roles = "Admin")]
        // public async Task<IActionResult> GetById([FromRoute] int id)
        // {
        //     var rocket = await _rocketRepo.GetByIdAsync(id);

        //     if (rocket == null)
        //     {
        //         return NotFound();
        //     }

        //     return Ok(rocket);
        // }

        // [HttpPost]
        // [Authorize(Roles = "Admin")]
        // public async Task<IActionResult> Create([FromBody] CreateRocketRequestDto rocketDto)
        // {
        //     var rocketModel = rocketDto.ToRocketFromCreateDTO();
        //     await _rocketRepo.CreateAsync(rocketModel);
        //     return CreatedAtAction(nameof(GetById), new { id = rocketModel.ID }, rocketModel.ToRocketDto());
        // }

        // [HttpPut("{id}")]
        // [Authorize(Roles = "User,Admin")]
        // public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateRocketRequestDto updateDto)
        // {
        //     var rocketModel = await _rocketRepo.UpdateAsync(id, updateDto);
        //     if (rocketModel == null)
        //     {
        //         return NotFound();
        //     }

        //     return Ok(rocketModel.ToRocketDto());
        // }

        // [HttpDelete("{id}")]
        // [Authorize(Roles = "User,Admin")]
        // public async Task<IActionResult> Delete([FromRoute] int id)
        // {
        //     var rocketModel = await _rocketRepo.DeleteAsync(id);
        //     if (rocketModel == null)
        //     {
        //         return NotFound();
        //     }

        //     return NoContent();
        // }
    }
}