using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using CukraszdaAPI.DTO;
using CommonLibrary.MODEL;

namespace CukraszdaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private static List<User> _users = new List<User>();

        [HttpPost("register")]
        public IActionResult Register(User user)
        {
            if (_users.Any(u => u.Email == user.Email))
                return BadRequest("Email already registered.");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash); // titkosítás
            _users.Add(user);
            return Ok(user);
        }

        [HttpPost("login")]
        public IActionResult Login(User loginUser)
        {
            var user = _users.FirstOrDefault(u => u.Email == loginUser.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginUser.PasswordHash, user.PasswordHash))
                return Unauthorized("Invalid credentials.");

            return Ok(user);
        }
    }

}
