using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroceryListApi.Models
{
    public class GroceryListItem
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("GroceryList")]
        public int GroceryListId { get; set; }

        // Grocery list that this item belongs to
        public virtual GroceryList GroceryList { get; set; }

        // Name of the list item
        public string Name { get; set; }
    }
}
