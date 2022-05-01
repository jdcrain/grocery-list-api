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
        public Task<GroceryListDto> GetAsync(int id);

        /// <summary>
        /// Creates a new Grocery list
        /// </summary>
        /// <param name="GroceryList">New grocery list to create</param>
        /// <returns>Grocery list that was persisted to the database</returns>
        public Task<GroceryListDto> CreateAsync(GroceryListDto GroceryList);

        /// <summary>
        /// Updates an existing grocery list
        /// </summary>
        /// <param name="GroceryList">Updated grocery list</param>
        /// <returns>Updated grocery list record</returns>
        public Task<GroceryListDto> UpdateAsync(GroceryListDto GroceryList);


        /// <summary>
        /// Deletes a grocery list
        /// </summary>
        /// <param name="id">Id of the grocery list to delete</param>
        /// <returns></returns>
        public Task DeleteAsync(int id);
    }
}
