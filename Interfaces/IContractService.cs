using Supplier_SRM_MVC.Dto;
using Supplier_SRM_MVC.Entities;

namespace Supplier_SRM_MVC.Interfaces
{
    public interface IContractService
    {
        Task<Contract> CreateContractAsync(int? supplierId, ContractCreateDto request);
    }
}
