using Azure.Core;
using SmartLibraryManagmentSystem.Models.Entities;
using SmartLibraryManagmentSystem.Repositories.Interfaces;
using SmartLibraryManagmentSystem.Services.Interfaces;
using SmartLibraryManagmentSystem.ViewModels.Catagory;

namespace SmartLibraryManagmentSystem.Services.Implementation
{
    public class CatagoryService : ICatagoryService
    {
        private readonly ICatagoryRepository _catagoryRepo;
        private readonly ILogger<CatagoryService> _logger;
        public CatagoryService(ICatagoryRepository catagoryRepo , ILogger<CatagoryService> logger)

        {
            _catagoryRepo = catagoryRepo;
            _logger = logger;
        }

        public async  Task<bool> CreateCatagorAsync(CatagoryCreateViewModel model)
        {
            try
            {
                var existingCatagory = await  _catagoryRepo.GetByNameAsync(model.Name);
                if(existingCatagory != null)
                {
                    _logger.LogWarning("Catagory is already Exist: {Name}", model.Name);
                    return false;
                }
                var catagory = new Catagory
                {
                    Name = model.Name.ToUpper(),
                };
                await _catagoryRepo.AddAsync(catagory);
                await _catagoryRepo.SaveAsync();
                _logger.LogInformation("{name} catagory created successful", model.Name);
                return true;   
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating catagory !");
                return false;
            }
        }

        public async  Task<bool> DeleteCatagoryAsync(int id)
        {
            try
            {
                var existingCatagory = await _catagoryRepo.GetByIdAsync(id);
                if( existingCatagory == null )
                {
                    _logger.LogError("Catagory not found!");
                    return false;
                  
                }
                _catagoryRepo.Delete(existingCatagory);
                await _catagoryRepo.SaveAsync();
                _logger.LogInformation("{catagoryName} catagory deleted successful!",
                    existingCatagory.Name);
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error  deleting catagory !");
                return false;
            }
        }

        public async Task<IEnumerable<CatagoryListViewModel>> GetAllCatagoriesAsync()
        {
            var catagoriesList=  await _catagoryRepo.GetAllAsync();
            return catagoriesList.Select(c => new CatagoryListViewModel
            {
                Id = c.Id,
                Name=c.Name,
            });
           
        }

        public async Task<CatagoryEditViewModel> GetById(int id)
        {
            try
            {
                var catagory = await _catagoryRepo.GetByIdAsync(id);
                if (catagory == null)
                {
                    _logger.LogInformation("Catagory are not available!");
                    return null;
                }
                _logger.LogInformation("Catagory are  found ");
                return new CatagoryEditViewModel
                {
                    Id = catagory.Id,
                    Name = catagory.Name
                };

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error Catagory search by Id!");
                return null;
            }
        }

        public async  Task<bool> UpdateCatagoryAsync(CatagoryEditViewModel model)
        {
            try
            {
                var catagory = await _catagoryRepo.GetByIdAsync(model.Id);
                if(catagory==null)
                {
                    _logger.LogWarning("Catagory are not found for update!");
                    return false;
                }
                catagory.Name = model.Name;
                _catagoryRepo.Update(catagory);
                await _catagoryRepo.SaveAsync();
                _logger.LogInformation("{catagoryName} catagory are updated successful", catagory.Name);
                return true;
            }
           catch(Exception e)
            {
                _logger.LogError(e, "Error are occured for catagory  update");
                return false;
            }
        }
    }
}
