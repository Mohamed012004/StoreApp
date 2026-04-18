using System.ComponentModel.DataAnnotations;

namespace Store.Route.Services.Abstractions.Auth
{
    public class LoginRequest
    {
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
