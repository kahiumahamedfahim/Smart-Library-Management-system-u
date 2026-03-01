namespace SmartLibraryManagmentSystem.Services.Interfaces
{
    public interface IFileService
    {
        Task<string?> SaveUserImageAsync(IFormFile? file);

    }
}
