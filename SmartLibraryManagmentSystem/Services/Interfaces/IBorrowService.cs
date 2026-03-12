using SmartLibraryManagmentSystem.ViewModels.Borrow;

namespace SmartLibraryManagmentSystem.Services.Interfaces
{
    public interface IBorrowService
    {
        Task<bool> RequestBorrowAsync(int UserId, int bookId);
        Task<IEnumerable<UserBorrowListViewModel>> GetUserBorrowsAsync(int userId);
        Task<bool> ApproveBorrowAsync(int borrowId);
        Task<bool> RejectBorrowAsync(int borrowId);
        Task<bool> RequestReturnAsync(int borrowId);
        Task<bool> ConfirmReturnAsync(int borrowId);
        Task<IEnumerable<AdminBorrowListViewModel>> GetAllBorrowRequestsAsync();
    }
}
