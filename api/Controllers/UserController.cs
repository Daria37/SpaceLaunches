using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using api.Models;

namespace api.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize(Roles = "Admin")] // Только для админов!
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;

        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        // Получить всех пользователей
        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userManager.Users.ToList();
            return Ok(users);
        }

        // Назначить роль пользователю
        [HttpPost("{userId}/roles")]
        public async Task<IActionResult> AssignRole(string userId, [FromBody] string role)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound("User not found");

            var result = await _userManager.AddToRoleAsync(user, role);
            if (!result.Succeeded) return BadRequest(result.Errors);

            return Ok($"Role '{role}' assigned to user {user.UserName}");
        }

        // Удалить пользователя
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded) return BadRequest(result.Errors);

            return NoContent();
        }
    }
}