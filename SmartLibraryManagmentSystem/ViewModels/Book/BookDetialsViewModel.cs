using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

namespace SmartLibraryManagmentSystem.ViewModels.Book
{
    public class BookDetialsViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
       public string Author { get; set; }
        public int Quantiry { get; set; }
        public int AvailableQuantity { get; set; }
        public string? ImageUrl { get; set; }
        public string? CatagoryName { get; set; }
    }
}
