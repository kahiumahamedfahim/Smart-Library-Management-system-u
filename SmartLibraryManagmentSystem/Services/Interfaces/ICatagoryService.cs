using SmartLibraryManagmentSystem.ViewModels.Catagory;

namespace SmartLibraryManagmentSystem.Services.Interfaces
{
    public interface ICatagoryService
    {
        Task<IEnumerable<CatagoryListViewModel>> GetAllCatagoriesAsync();
        Task<bool> CreateCatagorAsync(CatagoryCreateViewModel model);
        Task<bool> UpdateCatagoryAsync(CatagoryEditViewModel model);
        Task<bool> DeleteCatagoryAsync(int  id);  
        Task<CatagoryEditViewModel> GetById (int id);

    }
}
