namespace SmartLibraryManagmentSystem.Models.Entities
{
    public class BorrowRecord
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BookId { get; set; }
        public DateTime BorrowDate { get; set; } = DateTime.Now;
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; } = DateTime.MinValue;
        public decimal FineAmount { get; set; }
        //public BorrowRecord  Status { get; set; }
        public string Status { get; set; }
        public User? User { get; set; }
        public Book? Book { get; set; }
    }
}
