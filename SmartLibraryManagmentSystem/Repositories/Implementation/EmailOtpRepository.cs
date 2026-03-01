using Microsoft.EntityFrameworkCore;
using SmartLibraryManagmentSystem.Data;
using SmartLibraryManagmentSystem.Models.Entities;
using SmartLibraryManagmentSystem.Repositories.Interfaces;

namespace SmartLibraryManagmentSystem.Repositories.Implementation
{
    public class EmailOtpRepository : GenericRepository<EmailOtp>, IEmailOtpRepository
    {
        public EmailOtpRepository(AppDbContext context) : base(context)
        {
        }

        public async  Task<EmailOtp> GetValidOtpAsync(string email, string otp)
        {
            var result= await _dbSet.FirstOrDefaultAsync(e => e.Email == email && e.OtpCode == otp && e.ExpireTime > DateTime.UtcNow);
            return result;
        }
    }
}
