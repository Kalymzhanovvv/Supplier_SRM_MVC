using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Supplier_SRM_MVC.Entities
{
    [Table("HistorySubmissions")]
    public class HistorySubmissionsForSupplier
    {
        [Key]
        public int Id { get; set; }
        public int SubmissionId { get; set; }
        public string? CompanyName { get; set; }
        public string? CompanyEmail { get; set; }
        public string? CompanyPhoneNumber { get; set; }
        public AppUser? User { get; set; }
        public DateTime SubmissionDate { get; set; }
        public bool IsConfirmed { get; set; } = false;
    }
}
