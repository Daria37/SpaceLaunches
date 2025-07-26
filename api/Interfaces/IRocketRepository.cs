using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Dtos.Rocket;
using api.Models;

namespace api.Interfaces
{
    public interface IRocketRepository
    {
        Task<List<Rocket>> GetAllAsync();
        Task<Rocket?> GetByIdAsync(int id);
        Task<Rocket> CreateAsync(Rocket rocketModel);
        Task<Rocket?> UpdateAsync(int id, UpdateRocketRequestDto rocketDto);
        Task<Rocket?> DeleteAsync(int id);
    }
}