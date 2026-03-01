using SmartLibraryManagmentSystem.Models.Entities;
using SmartLibraryManagmentSystem.ViewModels.Authentication;

namespace SmartLibraryManagmentSystem.Services.Interfaces
{
    public interface IAuthService
    {
        Task<bool> RegisterAsync(RegisterViewModel model);
        Task<bool> VerifyOtpAsync (VerifyOtpViewModel model);
        Task<User> LoginAsync (LoginViewModel model);

    }
}
