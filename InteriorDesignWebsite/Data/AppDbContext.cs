using Microsoft.EntityFrameworkCore;
using InteriorDesignWebsite.Models;

namespace InteriorDesignWebsite.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // DbSet for ContactForm
        public DbSet<ContactForm> ContactForms { get; set; }

        // DbSet for BlogPosts (Added this)
        public DbSet<BlogPost> BlogPosts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Set precision and scale for EstimatedBudget in ContactForm
            modelBuilder.Entity<ContactForm>()
                .Property(c => c.EstimatedBudget)
                .HasColumnType("decimal(18,2)"); // Precision of 18 and scale of 2

            // Ensure ImageUrl is required in BlogPost
            modelBuilder.Entity<BlogPost>()
                .Property(b => b.ImageUrl)
                .IsRequired();
        }
    }
}
