using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SimpleAPI.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SimpleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class TokenController : ControllerBase
    {

        [HttpPost("login")]
        public IActionResult CreateToken()
        {
            var token = GenerateJwtToken();
            return Ok(new { token });
        }

        private string GenerateJwtToken()
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("this is my custom Secret key for authentication"));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "your_issuer",
                audience: "your_audience",
                claims: new List<Claim>(),
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }




        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaskItem>>> GetToken()
        {

            return Ok();
        }




        private JwtSecurityToken GenerateAccessToken(string userName)
        {

            //// Create a JWT
            //var token = new JwtSecurityToken(
            //    issuer: _configuration["JwtSettings:Issuer"],
            //    audience: _configuration["JwtSettings:Audience"],
            //    claims: claims,
            //    expires: DateTime.UtcNow.AddMinutes(1), // Token expiration time
            //    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"])),
            //        SecurityAlgorithms.HmacSha256)
            //);

            //return token;
            return null;
        }
    }
}
