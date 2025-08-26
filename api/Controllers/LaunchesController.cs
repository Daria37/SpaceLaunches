using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Launches;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
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
        private readonly ISpaceDevsService _spaceDevsService;
        private readonly ApplicationDBContext _context;

        public LaunchesController(ApplicationDBContext context, ISpaceDevsService spaceDevsService)
        {
            _spaceDevsService = spaceDevsService;
            _context = context;
        }

        [HttpGet("data")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<SpaceDevsLaunches>> GetLaunches()
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

        [HttpGet("{id}")]
        [Authorize(Roles = "User, Admin")]
        public async Task<ActionResult<Launches>> GetLaunchById(string id, CancellationToken ct)
        {
            try
            {
                
                var launch = await _spaceDevsService.GetLaunchByIdAsync(id);
                
                if (launch == null)
                {
                    return NotFound($"Launch with ID {id} not found");
                }
                
                return Ok(launch);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("after-2020")]
        [Authorize(Roles = "Admin, User")]
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

        [HttpGet("spacex")]
        [Authorize(Roles = "Admin, User")]
        public async Task<ActionResult<SpaceDevsLaunches>> GetLaunchesSpaceX()
        {
            try
            {
                var launches = await _spaceDevsService.GetLaunchesSpaceX();

                return Ok(launches);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<Launches>>> SearchLaunches(
            [FromQuery] string? searchTerm,
            [FromServices] IDistributedCache cache)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                var cacheKey = "launches_search_all";
                
                var cachedData = await cache.GetStringAsync(cacheKey);
                if (cachedData != null)
                {
                    return Ok(JsonSerializer.Deserialize<List<Launches>>(cachedData));
                }

                var allLaunches = await _context.Launches.ToListAsync();
                
                await cache.SetStringAsync(
                    cacheKey,
                    JsonSerializer.Serialize(allLaunches),
                    new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) }
                );

                return Ok(allLaunches);
            }

            var searchCacheKey = $"launches_search_{searchTerm.ToLower()}";

            var searchCachedData = await cache.GetStringAsync(searchCacheKey);
            if (searchCachedData != null)
            {
                return Ok(JsonSerializer.Deserialize<List<Launches>>(searchCachedData));
            }

            var searchTermLower = searchTerm.ToLower();
            var query = _context.Launches
                .Where(l => 
                    l.Name.ToLower().Contains(searchTermLower) ||
                    (l.RocketName != null && l.RocketName.ToLower().Contains(searchTermLower)) ||
                    (l.CountryCode != null && l.CountryCode.ToLower().Contains(searchTermLower)) ||
                    (l.Mission != null && l.Mission.ToLower().Contains(searchTermLower))
                );

            var launches = await query.ToListAsync();

            await cache.SetStringAsync(
                searchCacheKey,
                JsonSerializer.Serialize(launches),
                new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) }
            );

            return Ok(launches);
        }

        [HttpGet("filter")]
        [Authorize(Roles = "User,Admin")]
        public async Task<ActionResult<IEnumerable<Launches>>> FilterLaunches(
            [FromQuery] string? StatusCode,
            [FromServices] IDistributedCache cache)
        {
            var cacheKey = $"launches_search_{StatusCode}";

            var cachedData = await cache.GetStringAsync(cacheKey);
            if (cachedData != null)
            {
                return Ok(JsonSerializer.Deserialize<List<Launches>>(cachedData));
            }

            var query = _context.Launches.AsQueryable();

            if (!string.IsNullOrEmpty(StatusCode))
            {
                query = query.Where(l => l.Status == StatusCode);
            }

            var launches = await query.ToListAsync();

            await cache.SetStringAsync(
                cacheKey,
                JsonSerializer.Serialize(launches),
                new DistributedCacheEntryOptions { AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5) }
            );

            return Ok(launches);
        }
    }
}