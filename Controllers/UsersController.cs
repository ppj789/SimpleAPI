using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleAPI.Models.UserDTO;
using SimpleAPI.Services;

namespace SimpleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly UserService _userService;

        // Constructor injection for UserService
        public UsersController(UserService userService)
        {
            _userService = userService;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<IEnumerable<UserResponse>> GetUsers()
        {
            return await _userService.GetUsersAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserResponse>> GetUser(int id)
        {
            var user = await _userService.GetUserAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<UserResponse>> PutUser(int id, UpdateUserRequest updateUser)
        {

            await _userService.UpdateUserAsync(id, updateUser);



            return await _userService.UpdateUserAsync(id, updateUser); ;
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserResponse>> PostUser(CreateUserRequest createUser)
        {

            return await _userService.CreateUserAsync(createUser);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            await _userService.DeleteUserAsync(id);


            return NoContent();
        }

        // GET: api/Users/newApiKey/5
        [HttpGet("newApiKey/{id}")]
        public async Task<ActionResult<String>> GenerateNewApiKey(int id)
        {

            try
            {
                string apiKey = await _userService.GenerateApiKeyAsync(id);
                return Ok(apiKey);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }




        }

    }
}
