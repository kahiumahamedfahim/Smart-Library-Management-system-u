using System.ComponentModel.DataAnnotations;

namespace SmartLibraryManagmentSystem.ViewModels.Book
{
    public class BookCreateViewModel
    {
        [Required, MaxLength(200)]
        public string Title { get; set; }
        [Required, MaxLength(150)]
        public string Author { get; set; }

        [Required]
        public int Quantity { get; set; }
        [Required]
        public int CatagoryId { get; set; }
         public string? ImageUrl { get; set; }
    }
}
