namespace SmartLibraryManagmentSystem.ViewModels.Book
{
    public class BookListViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Author { get; set; } = null!;
        public int AvailableQuantity { get; set; }
        public string? ImageUrl { get; set; }
       public string CatagoryName { get; set; } = null!;
    }
}
