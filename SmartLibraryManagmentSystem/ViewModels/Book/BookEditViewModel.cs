using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel.DataAnnotations;

namespace SmartLibraryManagmentSystem.ViewModels.Book
{
    public class BookEditViewModel
    {
       public int Id { get; set; }
        [Required, MaxLength(200)]
        public string ? Title { get; set; }
        [Required, MaxLength(100)]
        public string ? Author { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int CatagoryId { get; set; }
        public string? ExistingImageUrl { get; set; }
        public IFormFile? Image { get; set; }
    }
}
