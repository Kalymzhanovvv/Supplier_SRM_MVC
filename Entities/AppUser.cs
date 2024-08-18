using Microsoft.AspNetCore.Identity;

namespace Supplier_SRM_MVC.Entities
{
    public class AppUser : IdentityUser
    {
        public string? Name { get; set; }
        public override bool EmailConfirmed { get; set; } = true;
    }       
}
