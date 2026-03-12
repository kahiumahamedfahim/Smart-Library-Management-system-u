using SmartLibraryManagmentSystem.Models.Entities;

namespace SmartLibraryManagmentSystem.Repositories.Interfaces
{
    public interface IBorrowRecordRepository : IGenericRepository<BorrowRecord>
    {
        Task<IEnumerable<BorrowRecord>> GetUserBorrowsAsync(int userId);
        Task<IEnumerable<BorrowRecord>> GetActiveBorrowsAsync();
        Task<IEnumerable<BorrowRecord>> GetOverDueAsync();
        Task<int> CountActiveAsync();
        Task<decimal> GetTotalFineAsync();
        Task<int> GetMonthlyBorrowCountAsync(int month, int year);
        Task<IEnumerable<BorrowRecord>> GetAllBorrowRequestsAsync();
    }
}
