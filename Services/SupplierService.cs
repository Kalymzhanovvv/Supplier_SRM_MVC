using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Supplier_SRM_MVC.Dto;
using Supplier_SRM_MVC.Entities;
using Supplier_SRM_MVC.Exceptions;
using Supplier_SRM_MVC.Interfaces;

namespace Supplier_SRM_MVC.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public SupplierService(
            ApplicationDbContext context,
            UserManager<AppUser> userManager,
            IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }

        public async Task<Supplier> RegisterSupplierAsync(SupplierCreateDto request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var currentUser = await _userManager.GetUserAsync(_contextAccessor.HttpContext!.User);

            var duplicateSupplier = await _context.Suppliers
                .Include(u => u.User)
                .Where(s => s.IsConfirmed != null && s.IsConfirmed == true)
                .FirstOrDefaultAsync(s => s.User!.Id == currentUser!.Id);

            if (duplicateSupplier != null)
            {
                throw new DuplicateWaitObjectException();
            }

            var newSupplier = new Supplier
            {
                CompanyName = request.CompanyName,
                CompanyEmail = request.CompanyEmail,
                CompanyPhoneNumber = request.CompanyPhoneNumber,
                IsConfirmed = false,
                User = currentUser,
            };
            _context.Suppliers.Add(newSupplier);
            await _context.SaveChangesAsync();

            var historySubmission = new HistorySubmissionsForSupplier
            {
                SubmissionId = newSupplier.Id,
                CompanyName = newSupplier.CompanyName,
                CompanyEmail = newSupplier.CompanyEmail,
                CompanyPhoneNumber = newSupplier.CompanyPhoneNumber,
                User = currentUser,
                SubmissionDate = DateTime.Now
            };

            _context.HistorySubmissions.Add(historySubmission);
            await _context.SaveChangesAsync();
            return newSupplier;
        }

        public async Task<Supplier> GetSupplierDataAsync()
        {
            var currentUser = await _userManager.GetUserAsync(_contextAccessor.HttpContext!.User);
            if (currentUser == null)
            {
                throw new NotFoundDataException();
            }

            var currentSupplier = await _context.Suppliers
                .Include(s => s.User)
                .FirstAsync(s => s.User!.Id == currentUser.Id);
            if (currentSupplier == null)
            {
                throw new NotFoundDataException();
            }

            return currentSupplier;
        }

        public async Task<List<Contract>> GetOffers()
        {
            var currentUser = await _userManager.GetUserAsync(_contextAccessor.HttpContext!.User);
            var currentSupplier = await _context.Suppliers
                .Include(s => s.User)
                .FirstOrDefaultAsync(s => s.User!.Id == currentUser!.Id);

            if (currentSupplier == null)
            {
                throw new NotFoundDataException();
            }

            return await _context.Contracts
                .Include(c => c.Customer)
                .Include(c => c.TermsAndConditions)
                .Where(c => c.Supplier!.Id == currentSupplier.Id && !c.IsConfirmed.HasValue)
                .ToListAsync();
        }


    }
}
