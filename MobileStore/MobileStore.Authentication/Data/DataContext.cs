using Microsoft.EntityFrameworkCore;
using MobileStore.Domain.Entities;

namespace MobileStore.Authentication.Domain.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().HasData(
                new User {Id = 1, Email = "user1@mail.ru", Password = "123456" },
                new User {Id = 2, Email = "user2@mail.ru", Password = "123456"}
                );
        }
    }
}
