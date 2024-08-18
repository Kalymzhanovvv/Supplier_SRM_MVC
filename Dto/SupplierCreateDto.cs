using System.ComponentModel.DataAnnotations;

namespace Supplier_SRM_MVC.Dto
{
    public class SupplierCreateDto
    {
        [Required]
        public string? CompanyName { get; set; }
        [Required]
        public string? CompanyEmail { get; set; }
        [Required]
        public string? CompanyPhoneNumber { get; set; }
    }
}
