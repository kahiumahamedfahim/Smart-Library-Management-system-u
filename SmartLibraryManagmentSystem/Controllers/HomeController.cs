using Microsoft.AspNetCore.Mvc;
using SmartLibraryManagmentSystem.Models;
using SmartLibraryManagmentSystem.Services.Interfaces;
using System.Diagnostics;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SmartLibraryManagmentSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBookService _bookService;

        public HomeController(ILogger<HomeController> logger,
            IBookService bookService)

        {
            _logger = logger;
            _bookService=bookService;
        }

        public async Task<IActionResult> Index()
        {
            int userId = 0;
            if (User.Identity.IsAuthenticated)
            {
                userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            }
            var books = await _bookService.GetBooksAsync(userId);

            return View(books);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
