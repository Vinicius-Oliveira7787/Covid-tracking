using System.Reflection;
using Domain;
using Domain.Countries;
using Microsoft.EntityFrameworkCore;

namespace Infra
{
    public class CovidContext : DbContext
    {
        public DbSet<Book> Book { get; set; }

        public DbSet<Publish> Publisher { get; set; }
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
