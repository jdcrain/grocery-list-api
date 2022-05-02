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

            modelBuilder.Entity<GroceryList>(entity =>
            {
                entity
                    .HasMany(list => list.GroceryListItems)
                    .WithOne(item => item.GroceryList)
                    .HasForeignKey(item => item.GroceryListId);

                entity
                    .HasOne(list => list.User)
                    .WithOne(user => user.GroceryList)
                    .HasForeignKey<GroceryList>(list => list.UserId);

                entity
                    .Property(list => list.Created)
                    .HasDefaultValueSql("current_timestamp at time zone 'utc'")
                    .ValueGeneratedOnAdd()
                    .Metadata
                    .SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Ignore);

                entity
                    .Property(list => list.LastModified)
                    .HasDefaultValueSql("current_timestamp at time zone 'utc'")
                    .ValueGeneratedOnAddOrUpdate();

                // postgres doesn't index FKs, so we'll manually add one for faster retrieves
                entity
                    .HasIndex(item => item.UserId);
            });


            modelBuilder.Entity<GroceryListItem>(entity =>
            {
                // postgres doesn't index FKs, so we'll manually add one for faster retrieves
                entity
                    .HasIndex(item => item.GroceryListId);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity
                    .Property(user => user.PasswordHash)
                    .IsRequired();

                entity
                    .HasIndex(user => user.Username)
                    .IsUnique();
            });
        }

        public DbSet<GroceryList> GroceryLists { get; set; }
        public DbSet<GroceryListItem> GroceryListItems { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
