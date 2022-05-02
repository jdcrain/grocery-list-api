using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GroceryListApi.DTOs;
using GroceryListApi.Exceptions;
using GroceryListApi.Repositories.GroceryList;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GroceryListAPI.Controllers
{
    [Route("groceryList")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public class GroceryListController : ControllerBase
    {
        private readonly IGroceryListRepository _groceryListRepository;

        public GroceryListController(IGroceryListRepository groceryListRepository)
        {
            _groceryListRepository = groceryListRepository;
        }

        [HttpGet]
        public async Task<ActionResult<GroceryListDto>> GetGroceryListAsync()
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            return await _groceryListRepository.GetByUserAsync(userId);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GroceryListDto>> GetGroceryListAsync(int id)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var groceryList = await _groceryListRepository.GetAsync(id, userId);

            if (groceryList == null)
            {
                return NotFound();
            }

            return groceryList;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroceryListAsync([FromBody] GroceryListDto groceryList)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);
            groceryList.UserId = userId;

            var createdGroceryList = await _groceryListRepository.CreateAsync(groceryList);

            return CreatedAtRoute(new { id = createdGroceryList.Id }, createdGroceryList);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateGroceryListAsync([FromBody] GroceryListDto groceryList)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            var updatedGroceryList = await _groceryListRepository.UpdateAsync(groceryList, userId);

            if (updatedGroceryList == null)
            {
                return BadRequest();
            }

            return Ok(updatedGroceryList);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroceryListAsync(int id)
        {
            int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value);

            try
            {
                await _groceryListRepository.DeleteAsync(id, userId);

                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}