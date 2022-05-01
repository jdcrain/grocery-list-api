using AutoMapper;
using GroceryListApi.DTOs;
using GroceryListApi.Exceptions;
using GroceryListApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryListApi.Repositories.GroceryList
{
    public class GroceryListRepository : IGroceryListRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public GroceryListRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<GroceryListDto> GetAsync(int id)
        {
            var groceryList = await _context.GroceryLists
                .Where(list => list.Id == id)
                .Include(list => list.GroceryListItems)
                .SingleOrDefaultAsync();

            if (groceryList == null)
            {
                return null;
            }

            return _mapper.Map<GroceryListDto>(groceryList);
        }

        /// <inheritdoc />
        public async Task<GroceryListDto> CreateAsync(GroceryListDto groceryList)
        {
            var groceryListModel = _mapper.Map<Models.GroceryList>(groceryList);

            var createdGroceryList = await _context.AddAsync(groceryListModel);

            await _context.SaveChangesAsync();

            return _mapper.Map<GroceryListDto>(createdGroceryList.Entity);
        }

        /// <inheritdoc />
        public async Task<GroceryListDto> UpdateAsync(GroceryListDto groceryList)
        {
            if (groceryList.Id == 0)
            {
                return null;
            }

            groceryList.LastModified = DateTime.UtcNow;

            var groceryListModel = _mapper.Map<Models.GroceryList>(groceryList);

            var existingGroceryListItems = await _context.GroceryListItems
                .Where(item => item.GroceryListId == groceryList.Id)
                .AsNoTracking()
                .ToListAsync();

            // Manually handle deletes since Update only performs adds and updates
            existingGroceryListItems.ForEach(item =>
            {
                if (!groceryListModel.GroceryListItems.Any(i => i.Id == item.Id))
                {
                    _context.GroceryListItems.Remove(item);
                }
            });

            var updatedGroceryList = _context.Update(groceryListModel);

            await _context.SaveChangesAsync();

            return _mapper.Map<GroceryListDto>(updatedGroceryList.Entity);
        }


        /// <inheritdoc />
        public async Task DeleteAsync(int id)
        {
            var groceryList = await _context.GroceryLists
                .Where(list => list.Id == id)
                .Include(list => list.GroceryListItems)
                .SingleOrDefaultAsync();

            if (groceryList == null)
            {
                throw new NotFoundException();
            }

            _context.Remove(groceryList);

            await _context.SaveChangesAsync();

            return;
        }
    }
}
