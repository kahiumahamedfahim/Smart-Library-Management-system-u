using System.ComponentModel.DataAnnotations;

namespace SmartLibraryManagmentSystem.ViewModels.Authentication
{
    public class RegisterViewModel
    {
        [Required]
        [MaxLength(100)]
        public string? Name { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [MinLength(6)]
        public string? Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string? ConfirmPassword { get; set; }
        public IFormFile? ProfileImage { get; set; }
    }
}
