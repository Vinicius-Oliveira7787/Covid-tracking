using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappings
{
    public class PublisherMapping : IEntityTypeConfiguration<Publish>
    {
        public void Configure(EntityTypeBuilder<Publish> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(book => book.Name).IsRequired();
        }
    }
}