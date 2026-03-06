using SmartLibraryManagmentSystem.ViewModels.Book;

namespace SmartLibraryManagmentSystem.Services.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookListViewModel>> GetAllAsync();
        Task<BookDetialsViewModel> GetBookById(int id);
        Task<bool> CreateBookAsync(BookCreateViewModel model);
        Task<bool> UpdateBookAsync(BookUpdateViewModel model);
        Task<bool> DeleteAsync(int id);

    }
}
