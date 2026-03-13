using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SmartLibraryManagmentSystem.Repositories.Interfaces;
using SmartLibraryManagmentSystem.Services.Interfaces;
using SmartLibraryManagmentSystem.ViewModels.Book;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace SmartLibraryManagmentSystem.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly ILogger<BookController> _logger;
        private readonly ICatagoryRepository _catagoryRepository;
        public BookController(IBookService bookService , 
            ICatagoryRepository catagoryRepo,
            ILogger<BookController> logger
            )
        {
            _bookService = bookService;
            _catagoryRepository = catagoryRepo;
            _logger = logger;
        }
        //book list
        public async Task <IActionResult> Index()
        {
            int userId = 0;
            if(User.Identity.IsAuthenticated)
            {
                userId= int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
            }
            var books = await _bookService.GetBooksAsync(userId);

            return View(books);
        }
        //Book Details
        public async Task<IActionResult> Details(int id)
        {
            var book = await _bookService.GetBookById(id);
            if(book==null)
            {
                _logger.LogInformation("Book not found!");
                return NotFound();
            }
            return View(book);
        }
        //Book Create 
        [Authorize(Roles ="Admin")]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var categories = await _catagoryRepository.GetAllAsync();
          

            var model = new BookCreateViewModel
            {
                Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
            };

            return View(model);
        }
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(BookCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = await _catagoryRepository.GetAllAsync();
                return View(model);
            }

            var result = await _bookService.CreateBookAsync(model);

            if (!result)
            {
                ViewBag.Categories = await _catagoryRepository.GetAllAsync();
                ModelState.AddModelError("", "Failed to create book.");
                return RedirectToAction("Create");
            }

            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _bookService.GetBookById(id);

            if (book == null)
            {
                _logger.LogError("Book not found");
                return NotFound();
            }

            var categories = await _catagoryRepository.GetAllAsync();

            var model = new BookUpdateViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                Quantity = book.Quantity,
                CatagoryId = book.CatagoryId,
                ExistingImage = book.ImageUrl,

                Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
            };

            return View(model);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(BookUpdateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _catagoryRepository.GetAllAsync();

                model.Categories = categories.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                });

                return View(model);
            }

            var result = await _bookService.UpdateBookAsync(model);

            if (!result)
            {
                _logger.LogError("Error in Book Edit controller");
                return NotFound();
            }

            return RedirectToAction("Index");
        }
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete (int id)
        {
            var result = await _bookService.DeleteAsync(id);
            if(!result)
            {
                return NotFound();
            }
            return RedirectToAction("index");
                
        }
    }
}
