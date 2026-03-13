using SmartLibraryManagmentSystem.Models.Entities;
using SmartLibraryManagmentSystem.Repositories.Interfaces;
using SmartLibraryManagmentSystem.Services.Interfaces;
using SmartLibraryManagmentSystem.ViewModels.Book;
using System.Net.WebSockets;

namespace SmartLibraryManagmentSystem.Services.Implementation
{
    public class BookService :IBookService
    {
        private readonly IBookRepository _bookRepo;
        private readonly ICatagoryRepository _catagoryRepo;
        private readonly IFileService _fileService;
        private readonly ILogger<BookService> _logger;
        private readonly IBorrowRecordRepository _borrowRecordRepo;
        public BookService(IBookRepository bookRepository,
            ICatagoryRepository catagoryRepository,
            IFileService fileService,
            ILogger<BookService> logger,
            IBorrowRecordRepository borrowRecordRepository 
            )
        {
            _bookRepo = bookRepository;
            _catagoryRepo = catagoryRepository;
            _fileService = fileService;
            _logger = logger;
            _borrowRecordRepo= borrowRecordRepository;
        }

        public async Task<bool> CreateBookAsync(BookCreateViewModel model)
        {
            try
            {
                var imageUrl = await _fileService.SaveImageAsync(model.ImageUrl, "Books");
                var book = new Book
                {
                    Title = model.Title,
                    Author = model.Author,
                    Quantiry = model.Quantity,
                    AvailableQuantity = model.Quantity,
                    ImageUrl = imageUrl,
                    CatagoryId = model.CatagoryId

                };
                await _bookRepo.AddAsync(book);
                await _bookRepo.SaveAsync();
                _logger.LogInformation("Book created : {Title}", model.Title);
                return true;

            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Error creating Book!");
                return false;
            }
        }

        public async  Task<bool> DeleteAsync(int id)
        {
            try
            {
             var book= await _bookRepo.GetByIdAsync(id);
                if(book==null)
                {
                    _logger.LogDebug("Book are not found!");
                    return false;
                }
                _bookRepo.Delete(book);
                await _bookRepo.SaveAsync();
                _logger.LogInformation($"deleted: {book.Title}");
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogWarning(ex, "Error Deleting Book");
                return false;
            }
        }

        public async Task<IEnumerable<BookListViewModel>> GetBooksAsync(int userId)
        {
            var books = await _bookRepo.GetAllAyncWithCatagory();

            var result = new List<BookListViewModel>();

            foreach (var book in books)
            {
                var activeBorrow = await _borrowRecordRepo
                    .HasActiveBorrowAsync(userId, book.Id);

                result.Add(new BookListViewModel
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    AvailableQuantity = book.AvailableQuantity,
                    ImageUrl = book.ImageUrl,
                    CatagoryName = book.Catagory?.Name ?? "Unknown",

                    // ⭐ Borrow button control
                    CanBorrow = !activeBorrow
                });
            }

            return result;
        }
        public async  Task<IEnumerable<BookListViewModel>> GetAllAsync()
        {
            var books = await _bookRepo.GetAllAyncWithCatagory();
            _logger.LogInformation("Book list successful enrol!");
            return books.Select(book => new BookListViewModel
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
                AvailableQuantity = book.AvailableQuantity,
                ImageUrl = book.ImageUrl,
                CatagoryName = book.Catagory?.Name ?? "Unknown"
            });
            
        }

        public async Task<BookDetialsViewModel> GetBookById(int id)
        {
            try
            {
                var book = await _bookRepo.GetWithCatagoryAsync(id);
                if (book == null)
                {
                    _logger.LogInformation("Book is null!");
                    return null;
                }
                
                // Normalize image URL path
                var imageUrl = book.ImageUrl;
                if (!string.IsNullOrEmpty(imageUrl) && !imageUrl.StartsWith("~"))
                {
                    imageUrl = "~/" + imageUrl.TrimStart('/');
                }
                
                return new BookDetialsViewModel
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    Quantity = book.Quantiry,
                    AvailableQuantity = book.AvailableQuantity,
                    ImageUrl = imageUrl,
                    CatagoryName = book.Catagory?.Name
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("Error found in Book getbyId");
                return null;
            }
        }

        public async Task<bool> UpdateBookAsync(BookUpdateViewModel model)
        {
            try
            {
                var book = await _bookRepo.GetByIdAsync(model.Id); // Await the task to get the Book object
                if (book == null)
                {
                    _logger.LogError("Book are null here ");
                    return false;
                }
                if (model.Image != null)
                {
                    var imageUrl = await _fileService.SaveImageAsync(model.Image, "Books");
                    book.ImageUrl = imageUrl;
                }
                // Update other properties if needed
                book.Title = model.Title;
                book.Author = model.Author;
                book.Quantiry = model.Quantity;
                book.CatagoryId = model.CatagoryId;

                _bookRepo.Update(book);
                await _bookRepo.SaveAsync();
                _logger.LogInformation($"Book updated: {book.Title}");
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating Book");
                return false;
            }
        }
              
        
    }
}
