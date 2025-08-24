using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using api.Dtos.Account;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(RoleManager<IdentityRole> roleManager, UserManager<AppUser> userManager, ITokenService tokenService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

            if (user == null) return Unauthorized("Invalid username!");

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded) return Unauthorized("Username not found and/or password incorrect");


            return Ok(
                new NewUserDto
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    Token = _tokenService.CreateToken(user)
                }
            );
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var appUser = new AppUser
            {
                UserName = registerDto.Username,
                Email = registerDto.Email
            };

            var createdUser = await _userManager.CreateAsync(appUser, registerDto.Password);

            if (createdUser.Succeeded)
            {
                // Назначаем роль "User" по умолчанию
                await _userManager.AddToRoleAsync(appUser, "User"); // <- Уже есть у вас

                return Ok(new NewUserDto
                {
                    UserName = appUser.UserName,
                    Email = appUser.Email,
                    Token = _tokenService.CreateToken(appUser)
                });
            }

            return BadRequest(createdUser.Errors);
        }

        // Временный эндпоинт для назначения роли (потом удалите)
        [HttpPost("make-me-admin")]
        public async Task<IActionResult> MakeMeAdmin()
        {
            var user = await _userManager.FindByEmailAsync("admin@example.com");
            if (user == null) return BadRequest("User not found");

            // Создаем роль, если её нет
            if (!await _roleManager.RoleExistsAsync("Admin"))
                await _roleManager.CreateAsync(new IdentityRole("Admin"));

            // Назначаем роль
            var result = await _userManager.AddToRoleAsync(user, "Admin");
            return result.Succeeded
                ? Ok("Now you are admin!")
                : BadRequest(result.Errors);
        }

        [HttpPost("fix-roles")]
        // [Authorize(Roles = "Admin")]
        public async Task<IActionResult> FixRoles()
        {
            var user = await _userManager.FindByNameAsync("admin");
            if (user == null) return BadRequest("User not found");

            // Удаляем все роли
            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);
            
            // Добавляем только Admin
            await _userManager.AddToRoleAsync(user, "Admin");
            
            return Ok("Roles fixed. User now has only Admin role");
        }
    }
}