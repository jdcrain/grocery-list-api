using Microsoft.EntityFrameworkCore;

namespace GroceryListApi.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GroceryList>()
                .HasMany(list => list.GroceryListItems)
                .WithOne(item => item.GroceryList)
                .HasForeignKey(item => item.GroceryListId);

            modelBuilder.Entity<GroceryList>()
                .Property(list => list.Created)
                .HasDefaultValueSql("current_timestamp at time zone 'utc'")
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<GroceryList>()
                .Property(list => list.LastModified)
                .HasDefaultValueSql("current_timestamp at time zone 'utc'")
                .ValueGeneratedOnAddOrUpdate();

            // postgres doesn't index FKs, so we'll manually add one for faster retrieves
            modelBuilder.Entity<GroceryListItem>()
                .HasIndex(item => item.GroceryListId);
        }

        public DbSet<GroceryList> GroceryLists { get; set; }
        public DbSet<GroceryListItem> GroceryListItems { get; set; }
    }
}
