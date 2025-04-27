using System.Text;
using CommonLibrary.MODEL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using Vizsgafeladat.MODEL;

namespace CukraszdaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ProductDbContext _context = new ProductDbContext();

        [HttpGet("ping")]
        public IActionResult Ping()  // Csak teszt
        {
            return Ok("API él, minden rendben!");
        }

        

            [HttpPost("register")]
            public async Task<ActionResult<User>> RegisterAsync([FromBody] User model)
            {
                // Ellenőrizzük, hogy az email cím már foglalt-e
                //var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);
                //if (existingUser != null)
                //{
                //    return BadRequest("Ez az email cím már regisztrálva van.");
                //}

                // Új felhasználó létrehozása
                var newUser = new User
                {
                    Email = model.Email,
                    PasswordHash = (model.PasswordHash), // Implementáld a saját jelszó hashelést
                    Name = model.Name // Ha nem kell Name, eltávolíthatod
                };

                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                return Ok(newUser);
            }

            // Példa jelszó hash-elésre
            //private string HashPassword(string password)
            //{
            //    using var sha256 = SHA256.Create();
            //    var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            //    return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            //}
        


        /// <summary>
        ///     A metódus biztosítja a Felhasználó bejelentkezését.
        ///  Sikeres bejelentkezés esetén naplózza a belépést.
        ///     - Id
        ///     - időpont
        ///     - IP cím
        ///     - Használt alkalmazás
        /// </summary>
        /// <param name="loginUser"> Bejelenntkezni kívánt felhasználó</param>
        /// <returns></returns>
        [HttpPost("login")]
        public IActionResult Login([FromBody] User model)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Email == model.Email && u.PasswordHash == model.PasswordHash);

            if (user == null)
            {
                return NotFound("Hibás e-mail vagy jelszó.");
            }

            try
            {
                // IP-cím lekérése
                var ipAddress = HttpContext.Connection.RemoteIpAddress?.ToString();

                // Alkalmazás azonosító (User-Agent fejlécből)
                var clientApp = Request.Headers["User-Agent"].ToString();

                // Bejelentkezés naplózása
                var log = new LoginLog
                {
                    UserId = user.Id,
                    LoginTime = DateTime.Now,
                    IpAddress = ipAddress,
                    ClientApp = clientApp
                };

                _context.LoginLogs.Add(log);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Hiba a naplózás közben: {ex.Message}");
                // A login ettől függetlenül sikeres marad
            }

            return Ok(user);
        }

        /// <summary>
        /// Jogosult -e az oldalra
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="directive"></param>
        /// <returns></returns>
        [HttpPost("permittedPages")]
        public IActionResult GrantAccess([FromQuery] int userId, [FromQuery] string directive)
        {
            var page = _context.Pages.FirstOrDefault(p => p.Directive == directive);
            if (page == null)
                return NotFound("Az oldal nem található.");

            var access = new UserAccess
            {
                UserId = userId,
                PageId = page.Id
            };

            _context.UserAccesses.Add(access);
            _context.SaveChanges();

            return Ok("Hozzáférés hozzáadva.");
        }

        /// <summary>
        ///  Az összes oldal, amelyre jogosult
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet("GetPermittedPages/{userId}")]
        public async Task<ActionResult<List<int>>> GetPermittedPages(int userId)
        {
            var pageIds = await _context.UserAccesses
                .Where(ua => ua.UserId == userId)
                .Select(ua => ua.PageId)
                .ToListAsync();

            return Ok(pageIds);
        }


        [HttpPost("debugjson")]
        public IActionResult DebugJson([FromBody] object raw) // Szintén teszt
        {
            return Ok($"Kapott nyers adat: {raw}");
        }

    }
}
