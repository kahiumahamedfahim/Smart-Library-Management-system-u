using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;

namespace SmartLibraryManagmentSystem.ViewModels.Book
{
    public class BookDetialsViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
       public string Author { get; set; }
        public int Quantity { get; set; }
        public int AvailableQuantity { get; set; }
        public string? ImageUrl { get; set; }
        public int CatagoryId { get; set; }
        public string? CatagoryName { get; set; }
    }
}
