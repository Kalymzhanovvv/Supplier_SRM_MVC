using Supplier_SRM_MVC.Entities;

namespace Supplier_SRM_MVC.Dto
{
    public class ContractCreateDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal ContractValue { get; set; }
        public IFormFile? TermsAndConditions { get; set; }
    }
}
