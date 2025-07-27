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
    public class AgencyRepository : IAgencyRepository
    {
        private readonly ApplicationDBContext _context;
        public AgencyRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<Agency>> GetAllAsync()
        {
            return await _context.Agency.ToListAsync();
        }
    }
}