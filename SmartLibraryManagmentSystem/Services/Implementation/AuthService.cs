using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using SmartLibraryManagmentSystem.Models.Entities;
using SmartLibraryManagmentSystem.Repositories.Interfaces;
using SmartLibraryManagmentSystem.Services.Interfaces;
using SmartLibraryManagmentSystem.ViewModels.Authentication;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace SmartLibraryManagmentSystem.Services.Implementation
{
    public class AuthService :IAuthService
    {
        private readonly IUserRepository _userRepop;
        private readonly IEmailOtpRepository _emailOtp;
        private readonly IEmailService _emailService;
        private readonly IFileService _fileSerivce;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly ILogger<AuthService> _Logger;
        public AuthService(IUserRepository userRepository,
            IEmailOtpRepository emailRepository,
            IEmailService emailService,
            IFileService fileService,
            IPasswordHasher<User> passwirdHasher,
            ILogger<AuthService> logger)

        {

            _userRepop = userRepository;
            _emailOtp = emailRepository;
            _emailService = emailService;
            _fileSerivce = fileService;
            _passwordHasher = passwirdHasher;
            _Logger = logger;

        }

        public async Task<User> LoginAsync(LoginViewModel model)
        {
           try
            {
                _Logger.LogInformation("Starting login process for email: {Email}", model.Email);
                var user= await _userRepop.GetByEmailAsync(model.Email);
                if(user == null)
                {
                    _Logger.LogWarning("Login faild , user not found : {Email}", model.Email);
                    return null;
                }
                if(!user.IsVerified)
                {
                    _Logger.LogWarning("Login faild , email not verified : {Email}", model.Email);
                    return null;
                }

                var result= _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, model.Password);
                if(result == PasswordVerificationResult.Failed)
                {
                    _Logger.LogWarning("Login faild , invalid password : {Email}", model.Email);
                    return null;
                }
                    _Logger.LogInformation("Login successful for email: {Email}", model.Email);
                return user;



            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error during login for email: {Email}", model.Email);
                return null;
            }
        }

        public async Task<bool> RegisterAsync(RegisterViewModel model)
        {
            try
            {
                _Logger.LogInformation("Starting registration process for email: {Email}", model.Email);
                var existingUser = await _userRepop.GetByEmailAsync(model.Email);
                if (existingUser != null)
                {
                    if (existingUser.IsVerified)
                    {
                        _Logger.LogWarning("Registration attempt with already registered email: {Email}", model.Email);
                        return false;
                    }
                    else
                    {
                        _Logger.LogInformation("Resendinng OTP to Unverified email: {Email}", model.Email);
                        await DeleteExistingOtpAsync(model.Email);
                        var resendOtp = GenerateOtp();
                        await _emailOtp.AddAsync(new EmailOtp
                        {
                            Email = model.Email,
                            OtpCode = resendOtp,
                            ExpireTime = DateTime.UtcNow.AddMinutes(2)
                        });
                        await _emailOtp.SaveAsync();
                        await _emailService.SendOtpAsync(model.Email, resendOtp);
                        return true;
                    }
                }
                var imageUrl = await _fileSerivce.SaveImageAsync(model.ProfileImage, "User");
                var user = new User
                {
                    Name = model.Name,
                    Email = model.Email,
                    Role = "User",
                    ProfileUrl = imageUrl,
                    IsVerified = false

                };
                user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);
                await _userRepop.AddAsync(user);
                await _userRepop.SaveAsync();
                var otp = GenerateOtp();
                await _emailOtp.AddAsync(new EmailOtp
                {
                    Email = model.Email,
                    OtpCode = otp,
                    ExpireTime = DateTime.UtcNow.AddMinutes(2)
                });
                await _emailOtp.SaveAsync();
                await _emailService.SendOtpAsync(model.Email, otp);

                _Logger.LogInformation("Registration successful for email: {Email}", model.Email);
                return true;

            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error during registration for email: {Email}", model.Email);
                return false;
            }
        }

        public async Task<bool> VerifyOtpAsync(VerifyOtpViewModel model)
        {
            try
            {
                _Logger.LogInformation("Starting OTP verification for email: {Email}", model.Email);
                var otpRecord= await _emailOtp.GetValidOtpAsync(model.Email, model.OtpCode);
                if(otpRecord == null)
                {
                    _Logger.LogWarning("Invalid OTP attempt for email: {Email}", model.Email);
                    return false;
                }
                var user= await _userRepop.GetByEmailAsync(model.Email);
                if(user == null)
                {
                    _Logger.LogWarning("User not found during Otp Verification  {Email}", model.Email);
                    return false;
                }
                user.IsVerified = true;
                _userRepop.Update(user);
                await _userRepop.SaveAsync();
                await DeleteExistingOtpAsync(model.Email);
                _Logger.LogInformation("OTP verification successful for email: {Email}", model.Email);
                return true;
            }
            catch (Exception ex)
            {
                _Logger.LogError(ex, "Error during OTP verification for email: {Email}", model.Email);
                return false;
            }   
        }
        private string GenerateOtp()
        {
            var bytes = new Byte[4];
           RandomNumberGenerator.Fill(bytes);
            var otp= BitConverter.ToUInt32(bytes,0)%1000000;
            return otp.ToString("D6");
        }
       private async Task DeleteExistingOtpAsync(string email)
        {
            var otps = await _emailOtp.GetAllAsync();
            var emailOtps = otps.Where(e => e.Email == email).ToList();
            foreach (var otp in emailOtps)
            {
               _emailOtp.Delete(otp);
            }
            await _emailOtp.SaveAsync();
        }
    }
}
