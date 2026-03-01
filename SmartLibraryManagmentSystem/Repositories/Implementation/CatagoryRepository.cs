using Microsoft.EntityFrameworkCore;
using SmartLibraryManagmentSystem.Data;
using SmartLibraryManagmentSystem.Models.Entities;
using SmartLibraryManagmentSystem.Repositories.Interfaces;

namespace SmartLibraryManagmentSystem.Repositories.Implementation
{
    public class CatagoryRepository : GenericRepository<Catagory>, ICatagoryRepository
    {
        public CatagoryRepository(AppDbContext context) : base(context)
        {
        }
        public async Task<Catagory?> GetByNameAsync(string name)
        {
            var result = await _dbSet.FirstOrDefaultAsync(c => c.Name == name);
            return result;
        }
        public async Task<IEnumerable<Catagory>> GetAllWithBooksAsync()
        {
            var result = await _dbSet.Include(c => c.Books).ToListAsync();
            return result;
        }
    }
}
