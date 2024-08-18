using System.ComponentModel.DataAnnotations;

namespace Supplier_SRM_MVC.Dto
{
    public class AppUserRegisterDto
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
