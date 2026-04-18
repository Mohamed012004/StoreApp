using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Store.Route.Services.Abstractions;
using Store.Route.Services.Abstractions.Auth;
using Store.Route.Shared.DTOs.Auth;
using System.Security.Claims;

namespace Store.Route.Presentation
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IServiceManager _serviceManager) : ControllerBase
    {
        [HttpPost("login")]  // POST: baseUrl/api/auth/login
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _serviceManager.AuthService.LoginAsync(request);
            return Ok(result);
        }

        [HttpPost("register")] // POST: baseUrl/api/auth/register
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var result = await _serviceManager.AuthService.RegisterAsync(request);
            return Ok(result);
        }


        // Check User Exist
        [HttpGet("EmailExists")]
        public async Task<IActionResult> CheckEmailExists(string email)
        {
            var result = await _serviceManager.AuthService.CheckEmailExistsAsync(email);
            return Ok(result);
        }

        // Get Currant User 
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetCurrentUser()
        {
            var emailCliam = User.FindFirst(ClaimTypes.Email);
            var result = await _serviceManager.AuthService.GetCurrentUserAsync(emailCliam.Value);
            return Ok(result);
        }

        // Get Currant User Address
        [HttpGet("Address")]
        [Authorize]
        public async Task<IActionResult> GetCurrentUserAddress()
        {
            var emailCliam = User.FindFirst(ClaimTypes.Email);
            var result = await _serviceManager.AuthService.GetCurrentUserAddressAsync(emailCliam.Value);
            return Ok(result);
        }
        // Update Currant User Address
        [HttpPut("Address")]
        [Authorize]
        public async Task<IActionResult> UpdateCurrentUserAddress(AddressDto request)
        {
            var emailCliam = User.FindFirst(ClaimTypes.Email);
            var result = await _serviceManager.AuthService.UpdateCurrentUserAddressAsync(request, emailCliam.Value);
            return Ok(result);
        }
    }
}
