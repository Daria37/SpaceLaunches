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
    public class UsersRepository : IUsersRepository
    {
        private readonly ApplicationDBContext _context;
        public UsersRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<List<Users>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }
}