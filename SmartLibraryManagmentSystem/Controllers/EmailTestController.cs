using Microsoft.AspNetCore.Mvc;
using SmartLibraryManagmentSystem.Services.Interfaces;

namespace SmartLibraryManagmentSystem.Controllers
{
    [Route("email-test")]
    public class EmailTestController : Controller
    {
       
        private readonly IEmailService _emailService;

        public EmailTestController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpGet("send")]
        public async Task<IActionResult> Send()
        {
            string otp = "123456";
            await _emailService.SendOtpAsync("kzontar24@gmail.com", otp);

            return Content("OTP Email Sent Successfully");
        }
    }
}
