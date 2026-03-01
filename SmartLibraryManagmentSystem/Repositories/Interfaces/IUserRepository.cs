using SmartLibraryManagmentSystem.Models.Entities;

namespace SmartLibraryManagmentSystem.Repositories.Interfaces
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
    }
}
