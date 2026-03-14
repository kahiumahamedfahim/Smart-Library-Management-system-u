using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SmartLibraryManagmentSystem.Services.Interfaces;
using System.Security.Claims;

namespace SmartLibraryManagmentSystem.Controllers
{
    [Authorize]
    public class BorrowController : Controller
    {
        private readonly IBorrowService _borrowService;
        private readonly ILogger<BorrowController> _logger;
        public BorrowController(IBorrowService borrowService,
            ILogger<BorrowController> logger)
        {
            _borrowService = borrowService;
            _logger = logger;
        }
        [Authorize(Roles ="User")]
        public async Task<IActionResult> Request(int bookId)
        {
            var userId= int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result= await _borrowService.RequestBorrowAsync(userId, bookId);
            if(!result)
            {
                TempData["Error"] = "Unable to Request borrow :";
            }
            return RedirectToAction("Index", "Home");
        }
        //myBorrows
        [Authorize(Roles ="User")]
        public async Task<IActionResult> MyBorrows()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var borrows= await _borrowService.GetUserBorrowsAsync(userId);
            return View(borrows);
        }
        //Request Return 
        [Authorize(Roles ="User")]
        public async Task<IActionResult> RequestReturn(int borrowId)
        {
            await _borrowService.RequestReturnAsync(borrowId);
            return RedirectToAction("MyBorrows");
        }
        //borrow Request Page 
        [Authorize(Roles ="Admin")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> BorrowRequests()
        {
            var requests = await _borrowService.GetAllBorrowRequestsAsync();

            return View(requests);
        }
        //approve Borrows
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Approve (int borrowId)
        {
            await _borrowService.ApproveBorrowAsync(borrowId);
            return RedirectToAction("BorrowRequests");
        }
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Reject (int borrowId)
        {
            await _borrowService.RejectBorrowAsync(borrowId);
            return RedirectToAction("BorrowRequests");
        }
        [Authorize(Roles ="Admin")]
        public async Task <IActionResult> ConfirmReturn(int borrowId)
        {
            await _borrowService.ConfirmReturnAsync(borrowId);
            return RedirectToAction("BorrowRequests");

        }

    }
}
