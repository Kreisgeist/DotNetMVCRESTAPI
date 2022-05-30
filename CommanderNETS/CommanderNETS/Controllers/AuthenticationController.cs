using CommanderNETS.Data;
using CommanderNETS.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace CommanderNETS.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly string _secretKey;

        public AuthenticationController(IConfiguration config)
        {
            _secretKey = config.GetSection("Settings").GetSection("SecretKey").ToString();
        }

        [Route("Validate")]
        [HttpPost]
        public ActionResult Validate([FromBody] User request)
        {
            if (request.email == "prueba@gmail.com" && request.password == "123")
            {
                var keyBytes = Encoding.ASCII.GetBytes(_secretKey);
                var claims = new ClaimsIdentity();

                claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, request.email));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = claims,
                    Expires = DateTime.UtcNow.AddMinutes(5),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
                };

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

                string tokenCreated = tokenHandler.WriteToken(tokenConfig);

                return Ok(new { token = tokenCreated });
            }

            return Unauthorized(new { token = ""});
        }
    }
}
