using System.Collections.Generic;
using System.Threading.Tasks;
using GroceryListApi.DTOs;
using GroceryListApi.Exceptions;
using GroceryListApi.Repositories.GroceryListItem;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GroceryListAPI.Controllers
{
    [Route("groceryListItem")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public class GroceryListItemController : ControllerBase
    {
        private readonly IGroceryListItemRepository _groceryListItemRepository;

        public GroceryListItemController(IGroceryListItemRepository groceryListItemRepository)
        {
            _groceryListItemRepository = groceryListItemRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroceryListItemDto>>> GetGroceryListItemsAsync()
        {
            return await _groceryListItemRepository.GetAllAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GroceryListItemDto>> GetGroceryListItemAsync(int id)
        {
            var groceryList = await _groceryListItemRepository.GetAsync(id);

            if (groceryList == null)
            {
                return NotFound();
            }

            return groceryList;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroceryListItemAsync([FromBody] GroceryListItemDto groceryListItem)
        {
            var createdGroceryListItem = await _groceryListItemRepository.CreateAsync(groceryListItem);

            return CreatedAtRoute(new { id = createdGroceryListItem.Id }, createdGroceryListItem);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateGroceryListItemAsync([FromBody] GroceryListItemDto groceryListItem)
        {
            var updatedGroceryListItem = await _groceryListItemRepository.UpdateAsync(groceryListItem);

            if (updatedGroceryListItem == null)
            {
                return BadRequest();
            }

            return Ok(updatedGroceryListItem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroceryListItemAsync(int id)
        {
            try
            {
                await _groceryListItemRepository.DeleteAsync(id);

                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}