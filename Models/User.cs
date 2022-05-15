using Microsoft.AspNetCore.Identity;
using DelieveryWebApplication;

namespace DelieveryWebApplication.Models
{
    public class User : IdentityUser
    {
        public int Year { get; set; }

    }
}