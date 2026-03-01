namespace SmartLibraryManagmentSystem.ViewModels.DashBoard
{
    public class DashBoardViewModel
    {
        public int TotalUsers { get; set; }
        public int TotalBooks { get; set; }
        public int ActiveBorrows { get; set; }
        public int OverDueBooks { get; set; }
        public decimal TotalFine { get; set; }
    }
}
