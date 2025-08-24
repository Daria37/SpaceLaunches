using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Agency;
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
    [Route("api/agency")]
    [ApiController]
    public class AgencyController : ControllerBase
    {
        private readonly IAgencyRepository _agencyRepo;
        private readonly ISpaceDevsService _spaceDevsService;
        private readonly ApplicationDBContext _context;
        public AgencyController(ApplicationDBContext context, IAgencyRepository agencyRepo, ISpaceDevsService spaceDevsService)
        {
            _agencyRepo = agencyRepo;
            _spaceDevsService = spaceDevsService;
            _context = context;
        }

        // [HttpGet]
        // [Authorize(Roles = "Admin")]
        // public async Task<IActionResult> GetAll()
        // {
        //     var agency = await _agencyRepo.GetAllAsync();
        //     var agencyDto = agency.Select(s => s.ToAgencyDto());

        //     return Ok(agencyDto);
        // }

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

        // [HttpGet("search_filter")]
        // [Authorize(Roles = "User,Admin")]
        // public async Task<ActionResult<IEnumerable<Agency>>> SearchAgency(
        //     [FromQuery] string? searchTerm,
        //     [FromQuery] string? countryCode,
        //     [FromServices] IDistributedCache cache)
        // {
        //     // Формируем ключ кэша на основе всех параметров фильтрации
        //     var cacheKey = $"launches_search_{searchTerm}_{countryCode}";

        //     // Проверяем кэш
        //     var cachedData = await cache.GetStringAsync(cacheKey);
        //     if (cachedData != null)
        //     {
        //         return Ok(JsonSerializer.Deserialize<List<Agency>>(cachedData));
        //     }

        //     // Запрос к БД с учетом фильтров
        //     var query = _context.Agency.AsQueryable();

        //     if (!string.IsNullOrEmpty(searchTerm))
        //     {
        //         query = query.Where(l => l.Name.Contains(searchTerm));
        //     }

        //     if (!string.IsNullOrEmpty(countryCode))
        //     {
        //         query = query.Where(l => l.CountryCode == countryCode);
        //     }

        //     var agency = await query.ToListAsync();

        //     // Сохраняем в кэш
        //     await cache.SetStringAsync(
        //         cacheKey,
        //         JsonSerializer.Serialize(agency),
        //         new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) }
        //     );

        //     return Ok(agency);
        // }
    }
}