using System.ComponentModel.DataAnnotations;

namespace SmartLibraryManagmentSystem.ViewModels.Authentication
{
    public class VerifyOtpViewModel
    {
        [Required]
        public string Email { get; set; } = string.Empty;
        [Required]
        public string OtpCode { get; set; } = string.Empty;
    }
}
