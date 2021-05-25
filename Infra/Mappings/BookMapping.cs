using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mappings
{
    public class BookMapping : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.HasKey(e => e.ISBN);
            builder.Property(book => book.Title).IsRequired();
            builder.HasOne(book => book.Publisher).WithMany(p => p.Books);
        }
    }
}