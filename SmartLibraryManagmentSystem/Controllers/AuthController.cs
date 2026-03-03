using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SmartLibraryManagmentSystem.Services.Interfaces;
using SmartLibraryManagmentSystem.ViewModels.Authentication;
using System.Runtime.InteropServices;
using System.Security.Claims;

namespace SmartLibraryManagmentSystem.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;
        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _authService.RegisterAsync(model);
            if (!result)
            {
                ModelState.AddModelError("", "Registration failed. Please try again.");
                return View(model);
            }
            TempData["Email"] = model.Email;
            return RedirectToAction("VerifyOtp");


        }
        [HttpGet]
        public IActionResult VerifyOtp()
        {
            var model = new VerifyOtpViewModel
            {
                Email = TempData["Email"]?.ToString()
            };
            return View(model);
        }
        [HttpPost]
        public async Task <IActionResult> VerifyOtp (VerifyOtpViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            var result=  await _authService.VerifyOtpAsync(model);
            if(!result)
            {
                ModelState.AddModelError("", "Invalid or Expired OTP");
                return View (model);
            }
            return RedirectToAction("Login");
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public  async Task <IActionResult> Login(LoginViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            var user= await _authService.LoginAsync(model);
            if(user==null)
            {
                ModelState.AddModelError("", "Invalid Credentials or accout not verfied! ");
                return View (model);
            }
            var claims = new List<Claim>
            {
                new Claim (ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim (ClaimTypes.Name, user.Name),
                new Claim (ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)

            };
            var claimsIdentity = new  ClaimsIdentity(claims,"LibraryCookie");
            await HttpContext.SignInAsync(
                "LibraryCookie",
                new ClaimsPrincipal(claimsIdentity));
            _logger.LogInformation("User logged in successful: {Email}", user.Email);
            return RedirectToAction("Index", "Home");
            
                
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("LibraryCookie");
            return RedirectToAction("Login");
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResendOtp(string email)
        {
            if (string.IsNullOrEmpty(email))
                return BadRequest();

            var model = new RegisterViewModel
            {
                Email = email
            };

            var result = await _authService.RegisterAsync(model);

            if (!result)
                return BadRequest("Unable to resend OTP.");

            return Ok();
        }

    }
}
