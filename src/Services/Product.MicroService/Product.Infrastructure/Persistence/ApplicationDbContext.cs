using Microsoft.EntityFrameworkCore;
using Product.Domain.Entities;

namespace Product.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Book>().HasData(new Book
            {
                ProductId = 1,
                Name = "Product one",
                Price = 15,
                Description = "Product des...",
                ImageUrl = "book.png",
                CategoryName = "Book"
            });
            modelBuilder.Entity<Book>().HasData(new Book
            {
                ProductId = 2,
                Name = "Product two",
                Price = 15,
                Description = "Product des...",
                ImageUrl = "book.png",
                CategoryName = "Book"
            });
            modelBuilder.Entity<Book>().HasData(new Book
            {
                ProductId = 3,
                Name = "Product three",
                Price = 15,
                Description = "Product des...",
                ImageUrl = "book.png",
                CategoryName = "Book"
            });
        }
    }
}
