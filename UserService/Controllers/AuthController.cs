using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using UserService.Database.Entities;
using UserService.IRepository;


namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private IAuthUserRepository _authUserRepository;

        public AuthController(IConfiguration configuration, 
                              IAuthUserRepository authUserRepository)
        {
            _configuration = configuration;
            _authUserRepository = authUserRepository;
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

            //AuthUser user = AuthenticateUser(authUser);
            AuthUser user = _authUserRepository.AuthenticateAuthUser(authUser);
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


        [HttpPost]
        [Route("authenticate-user")]
        [AllowAnonymous]
        public IActionResult AuthenticateAuthUser(AuthUser authUser)
        {
            if (authUser == null)
            {
                return BadRequest();
            }

            AuthUser user = _authUserRepository.AuthenticateAuthUser(authUser);
            if (user == null)
                return BadRequest();            
            return Ok(user);
            //return Unauthorized();
        }

        [HttpGet]
        [EnableQuery]
        [Route("get-all-authusers")]
        [AllowAnonymous]
        public IActionResult GetAllAuthUsers()
        { 
            var authUsers = _authUserRepository.GetAuthUsers();
            if(authUsers is null)
                return NotFound();  
            return Ok(authUsers);   
        }

        [HttpPost]
        [Route("add-authuser")]
        [AllowAnonymous]
        public IActionResult AddAuthUsers(AuthUser authUser)
        {
            try
            {
                if (authUser == null)
                {
                    return BadRequest();
                }
                var response = _authUserRepository.AddAuthUser(authUser);
                return StatusCode(StatusCodes.Status201Created, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }           
        }

        [HttpGet]
        [Route("get-authuser/{id}")]
        [AllowAnonymous]
        public IActionResult GetAuthUsersById(int id)
        {           
                var authUser = _authUserRepository.GetAuthUserById(id);
                return Ok(authUser);            
        }

        [HttpPut("update-authuser/{id}")]
        public IActionResult Put([FromBody] AuthUser request)
        {
            var result = _authUserRepository.UpdateAuthUser(request);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        
        [HttpDelete("delete-authuser/{id}")]
        public bool Delete(int id)
        {
            var result = _authUserRepository.DeleteAuthUser(id);
            return result;
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
