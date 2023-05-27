using Domain.eServicesPortal.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.eServicesPortal.Context
{
    public class ApplicationContext: IdentityDbContext<User>
    { 
        protected readonly IConfiguration Configuration;

        public ApplicationContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Connect to MySQL with connection string from app settings
            var connectionString = Configuration.GetConnectionString("MySqlConnection");

            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Customize table name for the user entity
            modelBuilder.Entity<User>().ToTable("users");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("userroles");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("userclaims");
            modelBuilder.Entity<IdentityRole>().ToTable("roles");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("roleclaims");
            
        }

        public DbSet<User> Users { get; set; }

   

       
    }
}

