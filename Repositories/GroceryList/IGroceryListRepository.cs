using GroceryListApi.DTOs;
using System.Threading.Tasks;

namespace GroceryListApi.Repositories.GroceryList
{
    public interface IGroceryListRepository
    {
        /// <summary>
        /// Retrieves a Grocery list by Id
        /// </summary>
        /// <param name="id">Id to retrieve the grocery list by</param>
        /// <returns>Grocery list with the given Id</returns>
        public Task<GroceryListDto> GetAsync(int id, int userId);

        /// <summary>
        /// Retrieves a Grocery list that belongs to the user
        /// </summary>
        /// <param name="id">User id to retrieve the grocery list by</param>
        /// <returns>Grocery list with the given user Id</returns>
        public Task<GroceryListDto> GetByUserAsync(int userId);

        /// <summary>
        /// Creates a new grocery list
        /// </summary>
        /// <param name="groceryList">New grocery list to create</param>
        /// <returns>Grocery list that was persisted to the database</returns>
        public Task<GroceryListDto> CreateAsync(GroceryListDto groceryList);

        /// <summary>
        /// Updates an existing grocery list
        /// </summary>
        /// <param name="groceryList">Updated grocery list</param>
        /// <returns>Updated grocery list record</returns>
        public Task<GroceryListDto> UpdateAsync(GroceryListDto groceryList, int userId);

        /// <summary>
        /// Deletes a grocery list
        /// </summary>
        /// <param name="id">Id of the grocery list to delete</param>
        /// <returns></returns>
        public Task DeleteAsync(int id, int userId);
    }
}
