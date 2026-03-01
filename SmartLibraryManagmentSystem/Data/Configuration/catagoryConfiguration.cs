using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartLibraryManagmentSystem.Models.Entities;

namespace SmartLibraryManagmentSystem.Data.Configuration
{
    public class catagoryConfiguration : IEntityTypeConfiguration<Catagory>
    {
        public void Configure(EntityTypeBuilder<Catagory> builder)
        {
            builder.Property(c => c.Name)
                 .IsRequired()
                 .HasMaxLength(20);
            builder.HasIndex(c => c.Name)
                .IsUnique();
            

        }
    }
}
