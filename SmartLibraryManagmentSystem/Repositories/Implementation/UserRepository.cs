using Microsoft.EntityFrameworkCore;
using SmartLibraryManagmentSystem.Data;
using SmartLibraryManagmentSystem.Models.Entities;
using SmartLibraryManagmentSystem.Repositories.Interfaces;

namespace SmartLibraryManagmentSystem.Repositories.Implementation
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AppDbContext context) : base (context)
        {
            
        }
        public async Task<User?> GetByEmailAsync(string email)
        {
            var result = await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
            return result;
        }
    }
}
