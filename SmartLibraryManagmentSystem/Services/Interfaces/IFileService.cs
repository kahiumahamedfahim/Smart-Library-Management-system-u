namespace SmartLibraryManagmentSystem.Services.Interfaces
{
    public interface IFileService
    {
        Task<string?> SaveImageAsync(IFormFile? file, string folderName);

    }
}
