using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
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
        public async Task<List<Launches>> GetAllAsync()
        {
            return await _context.Launches.ToListAsync();
        }
    }
}