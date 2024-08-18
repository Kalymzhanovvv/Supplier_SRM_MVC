using System.ComponentModel.DataAnnotations.Schema;

namespace Supplier_SRM_MVC.Entities
{
    [Table("Contracts")]
    public class Contract
    {
        public int Id { get; set; }
        public string? ContractNumber { get; set; }
        public Supplier? Supplier { get; set; }
        public AppUser? Customer { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal ContractValue { get; set; }      // для юзера
        public FileData? TermsAndConditions { get; set; }       // для юзера
        public DateTime SignedDate { get; set; }
        public  bool? IsConfirmed { get; set; }
    }
}
    