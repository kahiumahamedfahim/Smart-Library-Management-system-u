using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using SmartLibraryManagmentSystem.Data;
using SmartLibraryManagmentSystem.Models.Entities;
using SmartLibraryManagmentSystem.Repositories.Interfaces;

namespace SmartLibraryManagmentSystem.Repositories.Implementation
{
    public class BorrowRecordRepository : GenericRepository<BorrowRecord>, IBorrowRecordRepository
    {
        public BorrowRecordRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<int> CountActiveAsync()
        {
            var result = await _dbSet.CountAsync(b => b.Status == "Borrowed");
            return result;
        }

        public async Task<IEnumerable<BorrowRecord>> GetActiveBorrowsAsync()
        {
            var result = await _dbSet.Include(b => b.Book)
                  .Where(b => b.Status == "Borrowed")
                  .ToListAsync();
            return result;
        }

        public Task<int> GetMonthlyBorrowCountAsync(int month, int year)
        {
            var result = _dbSet.CountAsync(b => b.BorrowDate.Month == month && b.BorrowDate.Year == year);
            return result;
        }

        public async Task<IEnumerable<BorrowRecord>> GetOverDueAsync()
        {
            var result = await _dbSet.Include(b => b.Book)
                .Where(b => b.DueDate < DateTime.Now && b.Status == "Borrowed")
                .ToListAsync();
            return result;
        }

        public async Task<decimal> GetTotalFineAsync()
        {
            var result = await _dbSet.SumAsync(b => b.FineAmount);
            return result;
        }

        public async Task<IEnumerable<BorrowRecord>> GetUserBorrowsAsync(int userId)
        {
            var result = await _dbSet.Include(b => b.Book)
                .Where(b => b.UserId == userId)
                .ToListAsync();
            return result;
        }
        public async Task<IEnumerable<BorrowRecord>> GetAllBorrowRequestsAsync()
        {
            var result = await _dbSet.Include(b => b.Book)
                 .Include(b => b.User)
                 .OrderByDescending(b => b.BorrowDate)
         .ToListAsync();
            return result;

        }
        public async Task<bool> HasActiveBorrowAsync(int userId, int bookId)
        {
            var latestBorrow = await _dbSet.Where(b => b.UserId == userId
            && b.BookId == bookId)
                 .OrderByDescending(b => b.BorrowDate)
                 .FirstOrDefaultAsync();
            if (latestBorrow == null)
            {
                return false;
            }
            return latestBorrow.Status == "Requsted"
                || latestBorrow.Status == "Approved"
                || latestBorrow.Status == "Borrowed"
                || latestBorrow.Status == "Return Request"
                || latestBorrow.Status=="Pending";


        }
    }
}
