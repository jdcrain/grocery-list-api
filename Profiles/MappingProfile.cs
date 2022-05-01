using AutoMapper;
using GroceryListApi.DTOs;
using GroceryListApi.Models;

namespace GroceryListApi.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<GroceryListItem, GroceryListItemDto>()
                .ReverseMap();

            CreateMap<GroceryList, GroceryListDto>()
                .ReverseMap();
        }
    }
}
