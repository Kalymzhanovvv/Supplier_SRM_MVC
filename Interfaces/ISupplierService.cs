using Supplier_SRM_MVC.Dto;
using Supplier_SRM_MVC.Entities;

namespace Supplier_SRM_MVC.Interfaces
{
    public interface ISupplierService
    {
        Task<Supplier> RegisterSupplierAsync(SupplierCreateDto request);
        Task<Supplier> GetSupplierDataAsync();
        Task<List<Contract>> GetOffers();
    }
}
