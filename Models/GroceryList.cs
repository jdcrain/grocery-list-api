using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroceryListApi.Models
{
    public class GroceryList
    {
        public GroceryList()
        {
            GroceryListItems = new HashSet<GroceryListItem>();
        }

        [Key]
        public int Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastModified { get; set; }

        [Index(IsUnique = true)]
        public int UserId { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<GroceryListItem> GroceryListItems { get; set; }
    }
}
