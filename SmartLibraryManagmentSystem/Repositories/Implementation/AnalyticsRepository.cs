using Microsoft.EntityFrameworkCore;
using SmartLibraryManagmentSystem.Data;
using SmartLibraryManagmentSystem.Repositories.Interfaces;

namespace SmartLibraryManagmentSystem.Repositories.Implementation
{
    public class AnalyticsRepository : IAnalyticsRepository
    {
        private readonly AppDbContext _context;
        public AnalyticsRepository(AppDbContext context)
        {
             _context=context;
        }
        public async Task<int> GetTotalBooksAsync()
            {
                return await _context.Books.CountAsync();
             }
        public async Task<int> GetTotalUsersAsync()
        {
            return await _context.Users.CountAsync();
        }
         public async Task<int> GetTotalBorrowedBooksAsync()
        {
            return await _context.BorrowRecords.CountAsync(br => br.ReturnDate == null);
        }

        public async  Task<int> GetOverdueCountAsync()
        {
           var result= await _context.BorrowRecords.CountAsync(br => br.ReturnDate == null && br.DueDate < DateTime.UtcNow);
              return result;
        }
    }
}
