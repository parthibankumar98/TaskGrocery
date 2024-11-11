using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskGrocery.Model;

namespace TaskGrocery.Data
{
    public class GroceryDbContext : DbContext
    {
        public GroceryDbContext(DbContextOptions<GroceryDbContext> options)
            : base(options)
        {
        }

        public DbSet<GroceryDetails> GroceryDetails { get; set; }
        public DbSet<GroceryNameAndCategory> GroceryNameAndCategory { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<GroceryNameAndCategory>(entity =>
            {
                entity.HasNoKey();
                entity.ToTable("GroceryNameAndCategory"); 
            });

            modelBuilder.Entity<GroceryDetails>(entity =>
            {
                entity.HasNoKey();
                entity.ToTable("GroceryDetails");

            });
        }
    }
}
