namespace SmartLibraryManagmentSystem.ViewModels.Borrow
{
    public class UserBorrowListViewModel
    {
        public int BorrowId { get; set; }
        public string BookTitle { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public decimal FineAmount { get; set; }
        public string? Status { get; set; }
    }
}
