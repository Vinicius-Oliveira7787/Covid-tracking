using CovidTracking.API.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CovidTracking.Data.Configuration
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasKey(country => country.Id);
            builder.Property(country => country.ActiveCases).IsRequired();
            builder.Property(country => country.CountryName).IsRequired();
            builder.HasIndex(country => country.CountryName).IsUnique();
            builder.Property(country => country.LastUpdate).IsRequired();
            builder.Property(country => country.NewCases).IsRequired();
            builder.Property(country => country.NewDeaths).IsRequired();
            builder.Property(country => country.TotalCases).IsRequired();
            builder.Property(country => country.TotalDeaths).IsRequired();
            builder.Property(country => country.TotalRecovered).IsRequired();
        }
    }
}