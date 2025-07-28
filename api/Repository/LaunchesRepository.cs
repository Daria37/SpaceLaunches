using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Helpers;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class LaunchesRepository : ILaunchesRepository
    {
        private readonly ApplicationDBContext _context;
        public LaunchesRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<Launches>> GetAllAsync(QueryObject query)
        {
            var launches = _context.Launches.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Name))
            {
                launches = launches.Where(s => s.Name.Contains(query.Name));
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    launches = query.IsDecsending ? launches.OrderByDescending(s => s.Name) : launches.OrderBy(s => s.Name);
                }
            }

            var skipNumber = (query.PageNumber - 1) * query.PageSize;

            return await launches.Skip(skipNumber).Take(query.PageSize).ToListAsync();
        }
    }
}