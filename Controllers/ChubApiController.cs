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
    [Route("api/[controller]")]
    [ApiController]
    public class ChubApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ChubApiController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<SecurityToken>> login(UserLogin user)
        {
            var userDetails = await _context.User.FirstOrDefaultAsync(u => u.Email == user.Email); // Use FirstOrDefaultAsync instead of FirstOrDefault
         
            bool isValid = BCrypt.Net.BCrypt.Verify(user.Password, userDetails.Password);

            if (!isValid)
            {
                return NotFound();
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("yourSecretKey");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Email, user.Email) }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(token);
        }
    }
}
