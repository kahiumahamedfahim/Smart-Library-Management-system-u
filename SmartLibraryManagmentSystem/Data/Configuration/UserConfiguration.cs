using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartLibraryManagmentSystem.Models.Entities;

namespace SmartLibraryManagmentSystem.Data.Configuration
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(u => u.Email).IsRequired()
                .HasMaxLength(300);
            builder.Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(20);
            builder.HasIndex(u=> u.Email)
                .IsUnique();
            builder.Property(u => u.PasswordHash)
                .IsRequired();
            builder.Property(u => u.Role)
                .IsRequired()
                .HasMaxLength(20);
            builder.Property(u => u.ProfileUrl)
                .IsRequired()
                .HasMaxLength(300);


        }
    }
}
