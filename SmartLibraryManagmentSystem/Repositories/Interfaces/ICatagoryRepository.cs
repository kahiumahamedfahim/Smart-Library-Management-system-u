using SmartLibraryManagmentSystem.Models.Entities;

namespace SmartLibraryManagmentSystem.Repositories.Interfaces
{
    public interface ICatagoryRepository : IGenericRepository<Catagory>
    {
        Task<Catagory?> GetByNameAsync(string name);
        Task<IEnumerable<Catagory>> GetAllWithBooksAsync();
    }
}
