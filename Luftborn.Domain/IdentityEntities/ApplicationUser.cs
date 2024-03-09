using Microsoft.AspNetCore.Identity;

namespace Luftborn.Domain.IdentityEntities
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
