using BulkyWeb_Razor.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyWeb_Razor.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options
        ) : base(options)
        {
            
        }

        // Define DbSets for your entities here
        public DbSet<Category> Categories { get; set; }

        // Seed initial data
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Call the base method first, if needed
            modelBuilder.Entity<Category>().HasData (
                new Category { Id = 1, Name = "Books", DisplayOrder = 1 },
                new Category { Id = 2, Name = "Electronics", DisplayOrder = 2 },
                new Category { Id = 3, Name = "Clothing", DisplayOrder = 3 }
            );
        }
    }
}
