using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartLibraryManagmentSystem.Models.Entities;

namespace SmartLibraryManagmentSystem.Data.Configuration
{
    public class EmaIlOtpConfiguration : IEntityTypeConfiguration<EmailOtp>
    {
        public void Configure(EntityTypeBuilder<EmailOtp> builder)
        {
            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(e => e.OtpCode)
                .IsRequired()
                .HasMaxLength(10);
             
        }
    }
}
