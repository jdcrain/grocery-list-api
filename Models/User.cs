using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroceryListApi.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Username { get; set; }

        public string PasswordHash { get; set; }

        [ForeignKey("GroceryList")]
        public int GroceryListId { get; set; }

        public virtual GroceryList GroceryList { get; set; }
    }
}
