using Microsoft.EntityFrameworkCore;
using SmartLibraryManagmentSystem.Models.Entities;

namespace SmartLibraryManagmentSystem.Data
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
          {
            
          }
        public DbSet<User> Users { get; set; }

        public DbSet<Catagory> Categories { get; set; }

        public DbSet<Book> Books { get; set; }

        public DbSet<BorrowRecord> BorrowRecords { get; set; }

        public DbSet<EmailOtp> EmailOtps { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
