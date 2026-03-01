using SmartLibraryManagmentSystem.Models.Entities;

namespace SmartLibraryManagmentSystem.Repositories.Interfaces
{
    public interface IEmailOtpRepository : IGenericRepository<EmailOtp>
    {
        Task<EmailOtp> GetValidOtpAsync(string email, string otp);
    }
}
