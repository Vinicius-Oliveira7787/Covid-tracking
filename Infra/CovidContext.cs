using Domain;
using Microsoft.EntityFrameworkCore;

namespace Infra
{
    public class CovidContext : DbContext
    {
        public DbSet<Book> Book { get; set; }

        public DbSet<Publish> Publisher { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("server=127.0.0.1;user=user;password=password;database=db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Publish>(entity => {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Book>(entity => {
                entity.HasKey(e => e.ISBN);
                entity.Property(e => e.Title).IsRequired();
                entity.HasOne(d => d.Publisher)
                    .WithMany(p => p.Books);
            });
        }
    }
}
