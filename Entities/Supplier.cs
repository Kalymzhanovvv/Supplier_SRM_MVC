using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Supplier_SRM_MVC.Entities
{
    [Table("Suppliers")]
    public class Supplier
    {
        [Key]
        public int Id { get; set; }
        public string? CompanyName { get; set; }
        public string? CompanyEmail { get; set; }
        public string? CompanyPhoneNumber { get; set; }
        public DateTime RegistrationDate { get; set; }
        public bool? IsConfirmed { get; set; }
        public AppUser? User { get; set; }
    }
}
