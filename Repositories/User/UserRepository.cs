using AutoMapper;
using GroceryListApi.DTOs;
using GroceryListApi.Models;
using GroceryListApi.Repositories.GroceryList;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryListApi.Repositories.User
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UserRepository(AppDbContext context, IMapper mapper, IGroceryListRepository groceryListRepository)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserDto> GetAsync()
        {
            return new UserDto();
        }

        public Models.User GetByUsername(string username)
        {
            return _context.Users
                .Where(u => u.Username.Equals(username))
                .FirstOrDefault();
        }


        public async Task<UserDto> CreateAsync(UserDto user)
        {
            // create the user an empty list for convienience
            user.GroceryList = new GroceryListDto();

            var userModel = _mapper.Map<Models.User>(user);

            userModel.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.Password, 10);

            var createdUser = await _context.AddAsync(userModel);

            await _context.SaveChangesAsync();

            return _mapper.Map<UserDto>(createdUser.Entity);
        }
    }
}
