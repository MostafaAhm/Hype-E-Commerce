using Microsoft.AspNetCore.Identity;
using Product.Domain.Common;

namespace Product.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
