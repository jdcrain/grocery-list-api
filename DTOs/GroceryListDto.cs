using System;
using System.Collections.Generic;

namespace GroceryListApi.DTOs
{
    public class GroceryListDto
    {
        public int Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastModified { get; set; }

        public int UserId { get; set; }

        public List<GroceryListItemDto> GroceryListItems { get; set; }
    }
}
