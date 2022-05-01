namespace GroceryListApi.DTOs
{
    public class GroceryListItemDto
    {
        public int Id { get; set; }

        public int GroceryListId { get; set; }

        // Name of the grocery list item
        public string Name { get; set; }
    }
}
