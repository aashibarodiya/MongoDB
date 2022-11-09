using Microsoft.AspNetCore.Mvc;
using MongoRepository.Models;
using UserApi.Services;

namespace MongoRepository.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class UserController:ControllerBase
    {
        private UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<List<User>> Get() =>
            await _userService.GetAsync();

        [HttpPost]
        public async Task<ActionResult> Post(User newUser)
        {
            await _userService.CreateAsync(newUser);

            return CreatedAtAction(nameof(Get), new { id = newUser.Id }, newUser);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(string id)
        {
            var user=await _userService.GetAsync(id);

            if(user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

    
    }
}
