using Microsoft.AspNetCore.Identity;

namespace taskApi.Models
{
    public class ApplicationUser : IdentityUser

    {
        public int Id { get; }
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
