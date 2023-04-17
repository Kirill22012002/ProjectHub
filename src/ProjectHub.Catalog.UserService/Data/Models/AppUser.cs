using Microsoft.AspNetCore.Identity;

namespace ProjectHub.Catalog.UserService.Data.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}