using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Supplier_SRM_MVC.Entities;
using Supplier_SRM_MVC.Interfaces;
using Supplier_SRM_MVC.Utilities;

namespace Supplier_SRM_MVC.Services
{
    public class ManageService : IManageService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public ManageService(ApplicationDbContext context, UserManager<AppUser> userManager, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }

        public async Task<List<Supplier>> GetPendingApprovalSuppliersAsync()
        {
            return await _context.Suppliers
                    .Where(s => s.IsConfirmed == false)
                    .Include(u => u.User)
                    .ToListAsync();
        }

        // подтверждение поставщика
        public async Task<Supplier> ConfirmSupplier(string? verificationBtn, int? id)
        {
            var currentSupplier = await _context.Suppliers
                .Include(u => u.User)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (currentSupplier == null)
            {
                throw new Exception();
            }

            // данные админа, а не юзера
            var currentUser = await _userManager.FindByIdAsync(currentSupplier.User!.Id);


            if (verificationBtn == true.ToString())
            {
                currentSupplier.IsConfirmed = true;
                _context.Suppliers.Update(currentSupplier);
                await _context.SaveChangesAsync();

                // присвоил роль
                if (!await _userManager.IsInRoleAsync(currentUser!, Roles.Supplier))
                {
                    await _userManager.AddToRoleAsync(currentUser!, Roles.Supplier);
                    _context.AppUsers.Update(currentUser!);
                    await _context.SaveChangesAsync();
                }

                return currentSupplier;
            }
            else
            {
                _context.Suppliers.Remove(currentSupplier);
                await _context.SaveChangesAsync();
                return null!;
            }
        }
    }
}
