using SmartLibraryManagmentSystem.Models.Entities;

namespace SmartLibraryManagmentSystem.Repositories.Interfaces
{
    public interface IBookRepository : IGenericRepository<Book>
    {
        Task<Book?> GetWithCatagoryAsync(int id);
        Task<IEnumerable<Book>> SearchAsync(string ? keyword);
        Task<IEnumerable<Book>> GetPagedAsync(int page, int pageSize);
        Task<int> CountAsync();
        Task<IEnumerable<Book>> GetAllAyncWithCatagory();
    }
}
