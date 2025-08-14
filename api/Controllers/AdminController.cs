// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using api.Dtos.Account;
// using api.Dtos.Roles;
// using api.Interfaces;
// using api.Models;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;

// namespace api.Controllers
// {
//     [ApiController]
//     [Route("api/admin")]
//     [Authorize(Roles = "Admin")] // Доступ только для админов
//     public class AdminController : ControllerBase
//     {
//         private readonly UserManager<AppUser> _userManager;
//         private readonly RoleManager<IdentityRole> _roleManager;

//         public AdminController(
//             UserManager<AppUser> userManager,
//             RoleManager<IdentityRole> roleManager)
//         {
//             _userManager = userManager;
//             _roleManager = roleManager;
//         }

//         [HttpGet("users")]
//         public async Task<IActionResult> GetAllUsers()
//         {
//             var users = await _userManager.Users
//                 .Select(u => new
//                 {
//                     u.Id,
//                     u.UserName,
//                     u.Email,
//                     Roles = _userManager.GetRolesAsync(u).Result
//                 })
//                 .ToListAsync();

//             return Ok(users);
//         }
        
//         [HttpPost("assign-role")]
//         public async Task<IActionResult> AssignRole([FromBody] AssignRoleDto model)
//         {
//             var user = await _userManager.FindByIdAsync(model.UserId);
//             if (user == null) return NotFound("User not found");

//             if (!await _roleManager.RoleExistsAsync(model.RoleName))
//                 return BadRequest("Role does not exist");

//             var result = await _userManager.AddToRoleAsync(user, model.RoleName);
            
//             return result.Succeeded 
//                 ? Ok($"Role '{model.RoleName}' assigned to user {user.UserName}")
//                 : BadRequest(result.Errors);
//         }
//     }
// }