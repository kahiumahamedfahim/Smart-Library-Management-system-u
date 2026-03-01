using Microsoft.AspNetCore.Routing.Constraints;

namespace SmartLibraryManagmentSystem.ViewModels.Borrow
{
    public class AdminBorrowListViewModel
    {
        public string? UserName { get; set; }
        public string? BookTitle { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public string? Status { get; set; }
    }
}
