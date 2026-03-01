using System.ComponentModel.DataAnnotations;

namespace SmartLibraryManagmentSystem.ViewModels.Catagory
{
    public class CatagoryCreateViewModel
    {
        [Required,MaxLength(100)]
        public string? Name { get; set; }
    }
}
