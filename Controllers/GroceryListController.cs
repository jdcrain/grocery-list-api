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

        [HttpGet("{id}")]
        public async Task<ActionResult<GroceryListDto>> GetGroceryListAsync(int id)
        {
            var groceryList = await _groceryListRepository.GetAsync(id);

            if (groceryList == null)
            {
                return NotFound();
            }

            return groceryList;
        }

        [HttpPost]
        public async Task<IActionResult> CreateGroceryListAsync([FromBody] GroceryListDto groceryList)
        {
            var createdGroceryList = await _groceryListRepository.CreateAsync(groceryList);

            return CreatedAtRoute(new { id = createdGroceryList.Id }, createdGroceryList);
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateGroceryListAsync([FromBody] GroceryListDto groceryList)
        {
            var updatedGroceryList = await _groceryListRepository.UpdateAsync(groceryList);

            if (updatedGroceryList == null)
            {
                return BadRequest();
            }

            return Ok(updatedGroceryList);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroceryListAsync(int id)
        {
            try
            {
                await _groceryListRepository.DeleteAsync(id);

                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
    }
}