namespace SmartLibraryManagmentSystem.Models.Entities
{
    public class Book
    {
       public int Id { get; set; }
       public string? Title { get; set; }
       public string? Author { get; set; }
       public int Quantiry { get; set; }
        public int AvailableQuantity { get; set; }
        public string? ImageUrl { get; set; }
        public int CatagoryId { get; set; }
        public Catagory ?Catagory { get; set; }
        public ICollection<BorrowRecord>? BorrowRecords { get; set; }

    }
}
