using LoginAgreeYa.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LoginAgreeYa.AppDbContext
{
    public class AppDbContexts : IdentityDbContext<IdentityUser>
    {
        public AppDbContexts(DbContextOptions<AppDbContexts> options) : base(options)
        {
        }
    
        public DbSet<RegistrationModel> Registration { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\MSSQLSERVER01;Database=EmployeeDB;Trusted_Connection=True;TrustServerCertificate=True");

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<RegistrationModel>()
                .HasIndex(u => u.Email)
                .IsUnique();
            builder.Entity<RegistrationModel>()
               .HasIndex(u => u.LoginUser)
               .IsUnique();
            builder.Entity<RegistrationModel>()
               .HasIndex(u => u.PhoneNumber)
               .IsUnique();
            SeedRoles(builder);
        }
        private static void SeedRoles(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>().HasData
                (
                new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
                new IdentityRole() { Name = "User", ConcurrencyStamp = "2", NormalizedName = "User" },
                 new IdentityRole() { Name = "HR", ConcurrencyStamp = "3", NormalizedName = "HR" }

                );
        }
    }
}
