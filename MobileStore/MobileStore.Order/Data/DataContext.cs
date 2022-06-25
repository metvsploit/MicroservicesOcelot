using Microsoft.EntityFrameworkCore;
using MobileStore.Domain.Entities;

namespace MobileStore.Order.Domain.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<Ordering> Orders { get; set; }
    }
}
