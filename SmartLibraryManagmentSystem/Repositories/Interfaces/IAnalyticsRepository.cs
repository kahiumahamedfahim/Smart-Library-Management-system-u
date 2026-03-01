namespace SmartLibraryManagmentSystem.Repositories.Interfaces
{
    public interface IAnalyticsRepository 
    {
        Task<int> GetTotalUsersAsync();
        Task<int> GetTotalBooksAsync();
        Task<int> GetOverdueCountAsync();
    }
}
