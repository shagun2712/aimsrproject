using Microsoft.EntityFrameworkCore;
using InteriorDesignWebsite.Models;

namespace InteriorDesignWebsite.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ContactForm> ContactForms { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Set precision and scale for EstimatedBudget
            modelBuilder.Entity<ContactForm>()
                .Property(c => c.EstimatedBudget)
                .HasColumnType("decimal(18,2)"); // Precision of 18 and scale of 2
        }
    }
}
