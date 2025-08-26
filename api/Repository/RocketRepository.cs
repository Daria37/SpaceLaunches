using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Dtos.Rocket;
using api.Interfaces;
using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Repository
{
    public class RocketRepository : IRocketRepository
    {
        private readonly ApplicationDBContext _context;
        public RocketRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<Rocket> CreateAsync(Rocket rocketModel)
        {
            await _context.Rocket.AddAsync(rocketModel);
            await _context.SaveChangesAsync();
            return rocketModel;
        }

        public async Task<Rocket?> DeleteAsync(int id)
        {
            var rocketModel = await _context.Rocket.FirstOrDefaultAsync(x => x.ID == id);

            if (rocketModel == null)
            {
                return null;
            }

            _context.Rocket.Remove(rocketModel);
            await _context.SaveChangesAsync();
            return rocketModel;
        }

        public async Task<List<Rocket>> GetAllAsync()
        {
            return await _context.Rocket.ToListAsync();
        }

        public async Task<Rocket?> GetByIdAsync(int id)
        {
            return await _context.Rocket.FindAsync(id);
        }

        public async Task<Rocket?> UpdateAsync(int id, UpdateRocketRequestDto rocketDto)
        {
            var existingRocket = await _context.Rocket.FirstOrDefaultAsync(x => x.ID == id);
            if (existingRocket == null)
            {
                return null;
            }

            existingRocket.ID = rocketDto.ID;
            existingRocket.AgencyID = rocketDto.AgencyID;

            await _context.SaveChangesAsync();

            return existingRocket;
        }
    }
}