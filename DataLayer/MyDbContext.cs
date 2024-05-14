using DataLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLayer
{
    // dotnet ef migrations add Initial
    // dotnet ef database update
    public class MyDbContext : DbContext
    {

        private readonly string _windowsConnectionString = @"Data Source=NBKR004513;Initial Catalog=Lab5DatabaseImg8;Integrated Security=True;TrustServerCertificate=True";

        public DbSet<User> Users { get; set; }
        public DbSet<UserType> UserTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_windowsConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .HasOne(f => f.Type)
                .WithMany(c => c.Users)
                .HasForeignKey(f => f.TypeId);

            builder.Entity<UserType>().HasData(new UserType { Id = Guid.Parse("B5443DFF-4AE6-4C8B-54D1-08DC74373A42"), Name = "Admin" });
        }
    }
}
