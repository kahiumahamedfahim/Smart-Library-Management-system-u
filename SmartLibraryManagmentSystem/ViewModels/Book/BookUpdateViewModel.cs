using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.ComponentModel.DataAnnotations;

namespace SmartLibraryManagmentSystem.ViewModels.Book
{
    public class BookUpdateViewModel
    {
        public int Id { get; set; }
        [Required]
        public  string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int CatagoryId { get; set; } 
        public IFormFile? Image { get; set; }
        public string ? ExistingImage {  get; set; }
        public IEnumerable<SelectListItem>? Categories { get; set; }
    }
}
