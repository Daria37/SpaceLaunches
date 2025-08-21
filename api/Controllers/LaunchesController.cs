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
using Microsoft.AspNetCore.Authorization;
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
        private readonly IHttpClientFactory _httpClientFactory;

        public LaunchesController(ApplicationDBContext context, IHttpClientFactory httpClientFactory, ILaunchesRepository launchesRepo, ISpaceDevsService spaceDevsService)
        {
            _launchesRepo = launchesRepo;
            _spaceDevsService = spaceDevsService;
            _context = context;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("data")]
        [Authorize(Roles = "User, Admin")]
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

        [HttpGet("after-2020")]
        [Authorize(Roles = "Admin")] // Доступно для User и Admin
        public async Task<ActionResult<SpaceDevsLaunches>> GetLaunchesAfter2020()
        {
            try
            {
                var launches = await _spaceDevsService.GetLaunchesAfter2020Async();

                return Ok(launches);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("search_filter")]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<IEnumerable<Launches>>> SearchLaunches(
            [FromQuery] string? searchTerm,
            [FromQuery] string? countryCode,
            [FromServices] IDistributedCache cache)
        {
            // Формируем ключ кэша на основе всех параметров фильтрации
            var cacheKey = $"launches_search_{searchTerm}_{countryCode}";

            // Проверяем кэш
            var cachedData = await cache.GetStringAsync(cacheKey);
            if (cachedData != null)
            {
                return Ok(JsonSerializer.Deserialize<List<Launches>>(cachedData));
            }

            // Запрос к БД с учетом фильтров
            var query = _context.Launches.AsQueryable();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                query = query.Where(l => l.Name.Contains(searchTerm));
            }

            if (!string.IsNullOrEmpty(countryCode))
            {
                query = query.Where(l => l.CountryCode == countryCode);
            }

            var launches = await query.ToListAsync();

            // Сохраняем в кэш
            await cache.SetStringAsync(
                cacheKey,
                JsonSerializer.Serialize(launches),
                new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) }
            );

            return Ok(launches);
        }
    }
}