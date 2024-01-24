using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Project.Data.Configurations;

namespace Project.Data
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext() { }
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }  
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                .ApplyConfiguration(new CategoryConfiguration());
            builder
                .ApplyConfiguration(new ProductConfiguration());
        }
    }
}