using Supplier_SRM_MVC.Entities;

namespace Supplier_SRM_MVC.Interfaces
{
    public interface IManageService
    {
        Task<List<Supplier>> GetPendingApprovalSuppliersAsync();
        Task<Supplier> ConfirmSupplier(string? verificationBtn, int? id);
    }
}
