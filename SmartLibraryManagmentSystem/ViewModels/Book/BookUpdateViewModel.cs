using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.ComponentModel.DataAnnotations;

namespace SmartLibraryManagmentSystem.ViewModels.Book
{
    public class BookUpdateViewModel
    {
        
            public int Id { get; set; }

            public string Title { get; set; }
            public string Author { get; set; }
            public int Quantity { get; set; }

            public int CatagoryId { get; set; }

            // dropdown
            public IEnumerable<SelectListItem>? Categories { get; set; }

            // new upload
            public IFormFile? ImageFile { get; set; }

            // old image (DB)
            public string? ExistingImage { get; set; }
        
    }
}
