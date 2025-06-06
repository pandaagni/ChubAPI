using ChubAPI.Data;
using ChubAPI.Models.Entity;
using Microsoft.AspNetCore.Mvc;
using ChubAPI.Models.Interfaces;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;



namespace ChubAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<SecurityToken>> Login(UserLogin user)
        {
            var userDetails = await _context.User.FirstOrDefaultAsync(u => u.Email == user.Email); // Use FirstOrDefaultAsync instead of FirstOrDefault
            if (userDetails == null)
            {
                return NotFound();
            }
            bool isValid = BCrypt.Net.BCrypt.Verify(user.Password, userDetails.Password);

            if (!isValid)
            {
                return NotFound();
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("mySuperSecretKeymySuperSecretKey");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, user.Email) }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(token);
        }

        [HttpPost]
        [Route("signup")]
        public async Task<ActionResult<Guid>> Signup(User user)
        {
            // Check if email already exists
            var existingUser = await _context.User.FirstOrDefaultAsync(u => u.Email == user.Email);
            if (existingUser != null)
            {
                return StatusCode(405, "Email already registered.");
            }

            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return Ok(user.UserID);
        }

        [HttpGet]
        [Route("status")]
        public async Task<ActionResult<String>> Status()
        {
            return Ok("Success");
        }
    }
}
