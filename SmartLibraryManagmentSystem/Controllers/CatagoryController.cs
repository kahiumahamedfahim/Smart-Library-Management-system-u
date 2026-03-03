using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartLibraryManagmentSystem.Repositories.Interfaces;
using SmartLibraryManagmentSystem.Services.Interfaces;
using SmartLibraryManagmentSystem.ViewModels.Catagory;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace SmartLibraryManagmentSystem.Controllers
{
    [Authorize( Roles ="Admin")]
    public class CatagoryController : Controller
    {
        private readonly ICatagoryService _catagoryService;
        private readonly ILogger<CatagoryController> _logger;
        public CatagoryController(ICatagoryService catagoryService ,
            ILogger<CatagoryController> logger)
        {
            _catagoryService = catagoryService;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            var catagories = await _catagoryService.GetAllCatagoriesAsync();
            return View(catagories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create (CatagoryCreateViewModel model)
        {
            try
            {
                 if(!ModelState.IsValid)
                     {
                    _logger.LogWarning("Model state have issue so please solve this");
                       return View(model);
                     }
                var result = await _catagoryService.CreateCatagorAsync(model);
                 if(!result)
                {
                    _logger.LogWarning(" ", "catagory is already exist");
                    return View(model);
                }
                _logger.LogInformation("Catagory created Succefull!");
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error found in Catagory create controller!");
                return View(model);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var catagory = await _catagoryService.GetById(id);
            if(catagory==null)
            {
                _logger.LogWarning("Catagory  is null here !");
                return NotFound();

            }
            return View(catagory);

        }
        [HttpPost]
        public async Task<IActionResult> Edit (CatagoryEditViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogWarning("model  is invalid  found !");
                    return View(model);
                }
                else
                {
                       var result =await _catagoryService.UpdateCatagoryAsync(model);
                        if(!result)
                        {
                        _logger.LogWarning("some problem is found in  caragory Updateasync");
                        return NotFound();
                        }
                    _logger.LogInformation("Catagory update successful!");
                    return RedirectToAction("Index");
               }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error found in Edit method in edit controller!");
                return View(model);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Delete (int id)
        {
            var result= await _catagoryService.DeleteCatagoryAsync(id);
            if(!result)
            {
                return NotFound();
            }
            return RedirectToAction("Index");
        }
    }
}
