using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using SmartLibraryManagmentSystem.Models.Entities;
using SmartLibraryManagmentSystem.Repositories.Interfaces;
using SmartLibraryManagmentSystem.Services.Interfaces;
using SmartLibraryManagmentSystem.ViewModels.Borrow;

namespace SmartLibraryManagmentSystem.Services.Implementation
{
    public class BorrowService : IBorrowService
    {
        private readonly IBorrowRecordRepository _borrowRecordRepo;
        private readonly IBookRepository _bookRepo;
        private readonly ILogger<BorrowService> _logger;
        public BorrowService(IBookRepository bookRepo, ILogger<BorrowService> logger,
            IBorrowRecordRepository borrowRecordRepository)
        {
            _bookRepo = bookRepo;
            _logger = logger;
            _borrowRecordRepo = borrowRecordRepository;
        }
        public async Task<bool> ApproveBorrowAsync(int borrowId)
        {
            var borrow = await _borrowRecordRepo.GetByIdAsync(borrowId);
            if (borrow == null)
            {
                _logger.LogInformation("Borrow is empty");
                return false;
            }
            var book = await _bookRepo.GetByIdAsync(borrow.Book.Id);
            if (book.AvailableQuantity <= 0)
            {
                return false;
            }
            borrow.Status = "Borrowd";
            book.AvailableQuantity--;
            _borrowRecordRepo.Update(borrow);
            _bookRepo.Update(book);
            await _borrowRecordRepo.SaveAsync();
            return true;

        }

        public async Task<bool> ConfirmReturnAsync(int borrowId)
        {
            var borrow = await _borrowRecordRepo.GetByIdAsync(borrowId);
            if (borrow == null)
            {
                _logger.LogInformation("Borrow is empty here !");
                return false;
            }
            var book = await _bookRepo.GetByIdAsync(borrow.BookId);
            borrow.ReturnDate = DateTime.Now;
            borrow.Status = "Returned";
            if (borrow.ReturnDate > borrow.DueDate)
            {
                var lateDays = (borrow.ReturnDate.Value - borrow.DueDate).Days;
                borrow.FineAmount = lateDays * 10;
            }
            book.AvailableQuantity++;
            _borrowRecordRepo.Update(borrow);
            _bookRepo.Update(book);
            await _bookRepo.SaveAsync();
            return true;
        }

        public async Task<IEnumerable<UserBorrowListViewModel>> GetUserBorrowsAsync(int userId)
        {
            var borrows = await _borrowRecordRepo.GetUserBorrowsAsync(userId);
            return borrows.Select(br => new UserBorrowListViewModel
            {
                BookTitle = br.Book.Title,
                BorrowDate = br.BorrowDate,
                DueDate = br.DueDate,
                ReturnDate = br.ReturnDate,
                FineAmount = br.FineAmount,
                Status = br.Status.ToString()

            });
        }

        public async Task<bool> RejectBorrowAsync(int borrowId)
        {
            var borrow = await _borrowRecordRepo.GetByIdAsync(borrowId);
            if (borrow == null)
            {
                _logger.LogInformation("Borrow Is Empty here !");
                return false;
            }
            borrow.Status = "Rejected";
            _borrowRecordRepo.Update(borrow);
            await _borrowRecordRepo.SaveAsync();
            return true;
        }

        public async Task<bool> RequestBorrowAsync(int userId, int bookId)
        {
            try
            {
                var book = await _bookRepo.GetByIdAsync(bookId);

                if (book == null || book.AvailableQuantity <= 0)
                {
                    _logger.LogInformation("Book is not available!");
                    return false;
                }

               
                var alreadyBorrowed = await _borrowRecordRepo
                    .HasActiveBorrowAsync(userId, bookId);

                if (alreadyBorrowed)
                {
                    _logger.LogInformation("User already borrowed this book.");
                    return false;
                }

                var borrow = new BorrowRecord
                {
                    UserId = userId,
                    BookId = bookId,
                    BorrowDate = DateTime.Now,
                    DueDate = DateTime.Now.AddDays(7),
                    Status = "Pending"
                };

                await _borrowRecordRepo.AddAsync(borrow);
                await _borrowRecordRepo.SaveAsync();

                _logger.LogInformation("Borrow Request created for BookId {BookId}", bookId);

                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Creating borrow request");
                return false;
            }
        }

        public async Task<bool> RequestReturnAsync(int borrowId)
        {
            var borrow = await _borrowRecordRepo.GetByIdAsync(borrowId);
            if (borrow == null)
            {
                return false;
            }
            borrow.Status = "Return Request";
            _borrowRecordRepo.Update(borrow);
            await _borrowRecordRepo.SaveAsync();
            return true;

        }


        public async Task<IEnumerable<AdminBorrowListViewModel>> GetAllBorrowRequestsAsync()
        {
            var borrows = await _borrowRecordRepo.GetAllBorrowRequestsAsync();

            return borrows.Select(b => new AdminBorrowListViewModel
            {
                BorrowId = b.Id,
                BookTitle = b.Book?.Title,
                UserName = b.User?.Name,
                BorrowDate = b.BorrowDate,
                DueDate = b.DueDate,
                //ReturnDate = b.ReturnDate,
                //FineAmount = b.FineAmount,
                Status = b.Status
            }).ToList();
        }
    }
}
