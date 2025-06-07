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
using NuGet.Protocol;
using Microsoft.AspNetCore.Authorization;



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


        [HttpGet]
        [Route("status")]
        public async Task<ActionResult<String>> Status()
        {
            return Ok("Success");
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login(UserLogin user)
        {
            var userDetails = await _context.User.FirstOrDefaultAsync(u => u.Email == user.Email);
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
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = "your_issuer",      // Add this line
                Audience = "your_audience"   // Add this line
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return Ok(new
            {
                token = tokenString,
                userId = userDetails.UserID
            });
        }

        [HttpPost]
        [Route("signup")]
        public async Task<ActionResult<Guid>> Signup(User user)
        {
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

        [Authorize]
        [HttpGet]
        [Route("users/{UserId}")]
        public async Task<ActionResult> UserDetails(Guid UserId)
        {
            var user = await _context.User
                .Where(u => u.UserID == UserId)
                .Select(u => new { u.UserName, u.Email, u.Phone })
                .FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

    }
}
