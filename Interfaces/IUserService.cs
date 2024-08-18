using Supplier_SRM_MVC.Dto;
using Supplier_SRM_MVC.Entities;

namespace Supplier_SRM_MVC.Interfaces
{
    public interface IUserService
    {
        Task<AppUser> CreateUserAsync(AppUserRegisterDto request);
        Task<AppUser> LoginAsync(AppUserLoginDto request);
        Task<AppUser> GetUserDataAsync();
    }
}
