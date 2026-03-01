using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SmartLibraryManagmentSystem.Data;
using SmartLibraryManagmentSystem.Models.Entities;
using SmartLibraryManagmentSystem.Repositories.Interfaces;

namespace SmartLibraryManagmentSystem.Repositories.Implementation
{
    public class BookRepository  : GenericRepository<Book> , IBookRepository
    {
        public BookRepository(AppDbContext context) : base(context)
        {
            
        }

        public async  Task<int> CountAsync()
        {
            return await _dbSet.CountAsync();
            
        }

        public async  Task<IEnumerable<Book>> GetPagedAsync(int page, int pageSize)
        {
            var result= await _dbSet.Include(b=>b.Catagory)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return result;
        }

        public  async Task<Book?> GetWithCatagoryAsync(int id)
        {
           var result= await _dbSet.Include(b=>b.Catagory)
                                .FirstOrDefaultAsync(b=>b.Id==id);
            return result;
        }

        public async  Task<IEnumerable<Book>> SearchAsync(string? keyword)
        {
            if(string.IsNullOrEmpty(keyword))
            {
                return await _dbSet.Include(b => b.Catagory).ToListAsync();
            }
            var result = await _dbSet.Include(b=>b.Catagory)
                .Where (b=>b.Title.Contains(keyword) || b.Author.Contains(keyword))
                .ToListAsync();
            return result;
        }
    }
}
