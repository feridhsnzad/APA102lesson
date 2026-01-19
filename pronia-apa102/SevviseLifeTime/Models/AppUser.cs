using Microsoft.AspNetCore.Identity;

namespace FrontToBack.Models
{
    public class AppUser : IdentityUser
    {
        public string Name { get; set; }
        public string SurName { get; set; }

    }
}
