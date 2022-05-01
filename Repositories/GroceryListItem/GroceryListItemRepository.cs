using AutoMapper;
using GroceryListApi.DTOs;
using GroceryListApi.Exceptions;
using GroceryListApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryListApi.Repositories.GroceryListItem
{
    public class GroceryListItemRepository : IGroceryListItemRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public GroceryListItemRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<List<GroceryListItemDto>> GetAllAsync()
        {
            var groceryListItems = await _context.GroceryListItems.ToListAsync();

            return _mapper.Map<List<GroceryListItemDto>>(groceryListItems);
        }

        /// <inheritdoc />
        public async Task<GroceryListItemDto> GetAsync(int id)
        {
            var groceryListItem = await _context.GroceryListItems
                .Where(item => item.Id == id)
                .SingleOrDefaultAsync();

            if (groceryListItem == null)
            {
                return null;
            }

            return _mapper.Map<GroceryListItemDto>(groceryListItem);
        }

        /// <inheritdoc />
        public async Task<GroceryListItemDto> CreateAsync(GroceryListItemDto groceryListItem)
        {
            var groceryListItemModel = _mapper.Map<Models.GroceryListItem>(groceryListItem);

            var createdGroceryListItem = await _context.AddAsync(groceryListItemModel);

            await _context.SaveChangesAsync();

            return _mapper.Map<GroceryListItemDto>(createdGroceryListItem.Entity);
        }

        /// <inheritdoc />
        public async Task<GroceryListItemDto> UpdateAsync(GroceryListItemDto groceryListItem)
        {
            if (groceryListItem.Id == 0)
            {
                return null;
            }

            var groceryListItemModel = _mapper.Map<Models.GroceryListItem>(groceryListItem);

            var updatedGroceryListItem = _context.Update(groceryListItemModel);

            await _context.SaveChangesAsync();

            return _mapper.Map<GroceryListItemDto>(updatedGroceryListItem.Entity);
        }


        /// <inheritdoc />
        public async Task DeleteAsync(int id)
        {
            var groceryListItem = await _context.GroceryListItems
                .Where(item => item.Id == id)
                .SingleOrDefaultAsync();

            if (groceryListItem == null)
            {
                throw new NotFoundException();
            }

            _context.Remove(groceryListItem);

            await _context.SaveChangesAsync();

            return;
        }
    }
}
