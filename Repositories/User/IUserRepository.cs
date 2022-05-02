using GroceryListApi.DTOs;
using System.Threading.Tasks;

namespace GroceryListApi.Repositories.User
{
    public interface IUserRepository
    {
        public Task<UserDto> GetAsync();

        public Models.User GetByUsername(string username);

        public Task<UserDto> CreateAsync(UserDto user);
    }
}
