using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartLibraryManagmentSystem.Models.Entities;

namespace SmartLibraryManagmentSystem.Data.Configuration
{
    public class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.Property(b => b.Title)
                 .IsRequired()
                 .HasMaxLength(50);
            builder.Property(b=>b.Author)
                .IsRequired()
                .HasMaxLength(50);
            builder.HasOne(b=>b.Catagory)
                .WithMany(c=>c.Books)
                .HasForeignKey(b=>b.CatagoryId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
