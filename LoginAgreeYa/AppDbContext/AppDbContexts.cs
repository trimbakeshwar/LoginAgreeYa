using LoginAgreeYa.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace LoginAgreeYa.AppDbContext
{
    public class AppDbContexts : IdentityDbContext
    {
        public AppDbContexts(DbContextOptions<AppDbContexts> options) : base(options)
        {
        }
    
        public DbSet<CustomerModel> Customers { get; set; }
        public DbSet<Aspnetuser> Aspnetuser { get; set; }
       
        
       
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
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
