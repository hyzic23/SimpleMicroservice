using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Text;
using UserService.Database.Entities;




namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        [Route("security/create-token")]
        [AllowAnonymous]
        public IActionResult CreateToken(AuthUser authUser)
        { 
            if(authUser == null)
            {
                return BadRequest();
            }

            AuthUser user = AuthenticateUser(authUser);
            if (user == null)
                return BadRequest();

            string token = GenerateToken(authUser);
            if (!String.IsNullOrEmpty(token))
                return Ok(token);

            //if (authUser.Username == "admin" && authUser.Password == "Passw0rd123")
            //{               
            //    var issuer = _configuration.GetSection("Jwt:Key").Value;
            //    var audience = _configuration.GetSection("Jwt:Audience").Value;
            //    var key = Encoding.ASCII.GetBytes(_configuration.GetSection("Jwt:Key").Value);

            //    var tokenDescriptor = new SecurityTokenDescriptor
            //    {
            //        Subject = new System.Security.Claims.ClaimsIdentity(new[]
            //        { 
            //            new Claim("Id", Guid.NewGuid().ToString()),
            //            new Claim(JwtRegisteredClaimNames.Sub, authUser.Username),
            //            new Claim(JwtRegisteredClaimNames.Email, authUser.Username),
            //            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            //        }),
            //        Expires = DateTime.UtcNow.AddMinutes(5),
            //        Issuer = issuer,
            //        Audience = audience,
            //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
            //        SecurityAlgorithms.HmacSha512Signature)
            //    };

            //    var tokenHandler = new JwtSecurityTokenHandler();
            //    var token = tokenHandler.CreateToken(tokenDescriptor);
            //    var jwtToken = tokenHandler.WriteToken(token);
            //    var stringToken = tokenHandler.WriteToken(token);
            //    return Ok(jwtToken);
            //}

            return Unauthorized();
        }



        private string GenerateToken(AuthUser authUser)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Key").Value));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
           // var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration.GetSection("Jwt:Issuer").Value,
                _configuration.GetSection("Jwt:Issuer").Value,
                null,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token); 
        }

        private AuthUser AuthenticateUser(AuthUser authUser)
        {
            AuthUser response = null;
            if (authUser.Username == "admin" && authUser.Password == "Passw0rd123")
            {
                response = new AuthUser() { Username = "Admin" };
            }
            return response;
        }



    }
}
