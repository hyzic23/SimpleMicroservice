using Microsoft.AspNetCore.Mvc;
using UserService.Database;
using UserService.Database.Entities;
using UserService.IRepository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DatabaseContext dbContext;
        private readonly IUserRepository userRepository;
        public UserController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        // GET: api/<UserController>
        [HttpGet]
        public IEnumerable<User> Get()
        {
            return userRepository.GetUsers();
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public User Get(int id)
        {
            return userRepository.GetUserById(id);
        }

        // POST api/<UserController>
        [HttpPost]
        public IActionResult Post([FromBody] User request)
        {
            try
            {
                var response = userRepository.AddUser(request);
                return StatusCode(StatusCodes.Status201Created, response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public IActionResult Put([FromBody] User request)
        {
            var result = userRepository.UpdateUser(request);            
            return StatusCode(StatusCodes.Status200OK, result);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            var result = userRepository.DeleteUser(id);
            return result;
        }
    }
}
