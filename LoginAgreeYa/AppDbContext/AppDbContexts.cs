using LoginAgreeYa.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LoginAgreeYa.AppDbContext
{
    public class AppDbContexts : DbContext
    {
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
        }
    }
}
