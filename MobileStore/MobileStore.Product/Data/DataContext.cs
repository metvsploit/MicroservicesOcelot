using Microsoft.EntityFrameworkCore;
using MobileStore.Domain.Entities;

namespace MobileStore.Product.Domain.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {
           Database.EnsureDeleted();
           Database.EnsureCreated();
        }

        public DbSet<Smartphone> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Smartphone>().HasData(
                new Smartphone { Id = 1, Name = "Iphone 228", Brand = "Apple", Price = 1111  },
                new Smartphone { Id = 2, Name = "Samsong A41", Brand = "Samseng", Price = 2222 }
                );
        }
    }
}
