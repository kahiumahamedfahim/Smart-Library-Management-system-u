using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SmartLibraryManagmentSystem.Models.Entities;

namespace SmartLibraryManagmentSystem.Data.Configuration
{
    public class BorrwRecordConfiguration : IEntityTypeConfiguration<BorrowRecord>
    {
        

        public void Configure(EntityTypeBuilder<BorrowRecord> builder)
        {
            builder.Property(b => b.Status)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(b => b.FineAmount)
                .HasPrecision(10, 2);
            builder.HasOne(b => b.User)
                .WithMany(u => u.BorrowRecords)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(b => b.Book)
                .WithMany(b => b.BorrowRecords)
                .HasForeignKey(b => b.BookId);

        }
    }
}
