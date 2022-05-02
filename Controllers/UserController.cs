using System.Threading.Tasks;
using GroceryListApi.DTOs;
using GroceryListApi.Repositories.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GroceryListApi.Controllers
{
    [Route("user")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet()]
        public async Task<ActionResult<UserDto>> GetUserAsync()
        {
            var user = await _userRepository.GetAsync();

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUserAsync([FromBody] UserDto user)
        {
            var createdUser = await _userRepository.CreateAsync(user);

            return CreatedAtRoute(new { id = createdUser.Id }, createdUser);
        }
    }
}