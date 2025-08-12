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
        private readonly ApplicationDBContext _context;
        // private readonly RedisCacheService _redisCache;

        public LaunchesController(ApplicationDBContext context, ILaunchesRepository launchesRepo, ISpaceDevsService spaceDevsService)
        {
            _launchesRepo = launchesRepo;
            _spaceDevsService = spaceDevsService;
            _context = context;
            // _redisCache = redisCache;
            // , RedisCacheService redisCache
        }

        [HttpGet("data")]
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

        // [HttpGet("redis")]
        // public async Task<IActionResult> GetLaunches([FromQuery] string? search, [FromQuery] string? filter)
        // {
        //     // Генерируем ключ на основе параметров запроса
        //     string cacheKey = $"launches:{search}:{filter}";

        //     // Проверяем кеш
        //     var cachedLaunches = await _redisCache.GetFromCacheAsync<List<Launches>>(cacheKey);
        //     if (cachedLaunches != null)
        //     {
        //         return Ok(cachedLaunches);
        //     }

        //     // Если нет в кеше — запрашиваем из БД
        //     var launches = await _spaceDevsService.GetLaunchesAsync(search, filter);

        //     // Сохраняем в кеш на 5 минут
        //     await _redisCache.SaveToCacheAsync(cacheKey, launches, TimeSpan.FromMinutes(5));

        //     return Ok(launches);
        // }

        // [HttpPost]
        // public async Task<IActionResult> AddLaunch([FromBody] Launches launch)
        // {
        //     var result = await _spaceDevsService.GetLaunchesAsync();

        //     // Инвалидируем кеш (если добавили новый запуск)
        //     await _redisCache.RemoveFromCacheAsync("launches:*"); // Можно реализовать удаление по паттерну

        //     return Ok(result);
        // }
    
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Launches>>> SearchLaunches(
            [FromQuery] string searchTerm,
            [FromServices] IDistributedCache cache)
        {
            var cacheKey = $"launches_search_{searchTerm}";

            // Пытаемся получить данные из кеша
            var cachedData = await cache.GetStringAsync(cacheKey);
            if (cachedData != null)
            {
                return Ok(JsonSerializer.Deserialize<List<Launches>>(cachedData));
            }

            // Если нет в кеше, получаем из базы
            var launches = await _context.Launches
                .Where(l => l.Name.Contains(searchTerm) || l.Name.Contains(searchTerm))
                .Where(l => l.Name.ToLower().Contains(searchTerm.ToLower()))
                .ToListAsync();

            // Сохраняем в кеш на 5 минут
            await cache.SetStringAsync(cacheKey, 
                JsonSerializer.Serialize(launches),
                new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                });

            return Ok(launches);
        }
    }
}