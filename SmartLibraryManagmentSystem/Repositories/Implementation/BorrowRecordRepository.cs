using Microsoft.EntityFrameworkCore;
using SmartLibraryManagmentSystem.Data;
using SmartLibraryManagmentSystem.Models.Entities;
using SmartLibraryManagmentSystem.Repositories.Interfaces;

namespace SmartLibraryManagmentSystem.Repositories.Implementation
{
    public class BorrowRecordRepository: GenericRepository<BorrowRecord>, IBorrowRecordRepository
      {
        public BorrowRecordRepository(AppDbContext context) : base(context)
        {
        }

        public async  Task<int> CountActiveAsync()
        {
            var result= await _dbSet.CountAsync(b => b.Status == "Borrowed");
            return result;
        }

        public async Task<IEnumerable<BorrowRecord>> GetActiveBorrowsAsync()
        {
          var result= await _dbSet.Include(b=>b.Book)
                .Where(b => b.Status == "Borrowed")
                .ToListAsync();
            return result;
        }

        public Task<int> GetMonthlyBorrowCountAsync(int month, int year)
        {
                var result= _dbSet.CountAsync(b => b.BorrowDate.Month == month && b.BorrowDate.Year == year);
            return result;
        }

        public async Task<IEnumerable<BorrowRecord>> GetOverDueAsync()
        {
            var result= await _dbSet.Include(b=>b.Book)
                .Where(b=>b.DueDate<DateTime.Now && b.Status=="Borrowed")
                .ToListAsync();
            return result;
        }

        public async  Task<decimal> GetTotalFineAsync()
        {
            var result=  await _dbSet.SumAsync(b => b.FineAmount);
            return result;
        }

        public async  Task<IEnumerable<BorrowRecord>> GetUserBorrowsAsync(int userId)
        {
            var result = await _dbSet.Include(b=>b.Book)
                .Where(b => b.UserId == userId)
                .ToListAsync();
            return result;
        }
    }
}
