using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Launches;
using api.Helpers;
using api.Interfaces;
using api.Mappers;
using api.Models;
using api.Service;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
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
        private readonly ApplicationDBContext _context;

        public LaunchesController(ApplicationDBContext context, ILaunchesRepository launchesRepo, ISpaceDevsService spaceDevsService, ILogger<LaunchesController> logger)
        {
            _launchesRepo = launchesRepo;
            _spaceDevsService = spaceDevsService;
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<SpaceDevsLaunches>> GetLaunches(CancellationToken ct)
        {
            try
            {
                var launches = await _spaceDevsService.GetLaunchesAsync();

                return Ok(launches);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // [HttpGet("search")]
        // public async Task<ActionResult<IEnumerable<Launches>>> SearchLaunches(
        //     [FromQuery] string searchTerm,
        //     [FromServices] IDistributedCache cache)
        // {
        //     var cacheKey = $"launches_search_{searchTerm}";
            
        //     // Пытаемся получить данные из кеша
        //     var cachedData = await cache.GetStringAsync(cacheKey);
        //     if (cachedData != null)
        //     {
        //         return Ok(JsonSerializer.Deserialize<List<Launches>>(cachedData));
        //     }

        //     // Если нет в кеше, получаем из базы
        //     var launches = await _context.Launches
        //         .Where(l => l.Name.Contains(searchTerm) || l.Name.Contains(searchTerm))
        //         .Where(l => l.Name.ToLower().Contains(searchTerm.ToLower()))
        //         .ToListAsync();

        //     // Сохраняем в кеш на 5 минут
        //     await cache.SetStringAsync(cacheKey, 
        //         JsonSerializer.Serialize(launches),
        //         new DistributedCacheEntryOptions
        //         {
        //             AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
        //         });

        //     return Ok(launches);
        // }
    }
}