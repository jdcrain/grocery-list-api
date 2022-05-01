using GroceryListApi.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GroceryListApi.Repositories.GroceryListItem
{
    public interface IGroceryListItemRepository
    {
        /// <summary>
        /// Retrieves all grocery list items
        /// </summary>
        /// <returns>Grocery list items in the database</returns>
        public Task<List<GroceryListItemDto>> GetAllAsync();

        /// <summary>
        /// Retrieves a Grocery list item by Id
        /// </summary>
        /// <param name="id">Id to retrieve the grocery list item by</param>
        /// <returns>Grocery list item with the given Id</returns>
        public Task<GroceryListItemDto> GetAsync(int id);

        /// <summary>
        /// Creates a new grocery list item
        /// </summary>
        /// <param name="groceryListItem">New grocery list item to create</param>
        /// <returns>Grocery list item that was persisted to the database</returns>
        public Task<GroceryListItemDto> CreateAsync(GroceryListItemDto groceryListItem);

        /// <summary>
        /// Updates an existing grocery list item
        /// </summary>
        /// <param name="groceryListItem">Updated grocery list item</param>
        /// <returns>Updated grocery list item record</returns>
        public Task<GroceryListItemDto> UpdateAsync(GroceryListItemDto groceryListItem);


        /// <summary>
        /// Deletes a grocery list item
        /// </summary>
        /// <param name="id">Id of the grocery list item to delete</param>
        /// <returns></returns>
        public Task DeleteAsync(int id);
    }
}
