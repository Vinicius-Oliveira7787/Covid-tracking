using System.Reflection;
using CovidTracking.API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CovidTracking.Data
{
    public class CovidContext : DbContext
    {
        public DbSet<Country> Countries { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=127.0.0.1;user=user;password=password;database=db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                Assembly.GetExecutingAssembly()
            );
        }
    }
}
