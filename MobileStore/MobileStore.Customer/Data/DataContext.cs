using Microsoft.EntityFrameworkCore;
using MobileStore.Domain.Entities;

namespace MobileStore.Customer
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<Person> Customers { get; set; }
    }
}
