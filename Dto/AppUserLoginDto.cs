using System.ComponentModel.DataAnnotations;

namespace Supplier_SRM_MVC.Dto
{
    public class AppUserLoginDto
    {
        [Required]
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

    }
}
