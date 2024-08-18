using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Supplier_SRM_MVC.Dto;
using Supplier_SRM_MVC.Entities;
using Supplier_SRM_MVC.Exceptions;
using Supplier_SRM_MVC.Interfaces;
using Supplier_SRM_MVC.Utilities;

namespace Supplier_SRM_MVC.Services
{
    public class ContractService : IContractService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public ContractService(ApplicationDbContext context, UserManager<AppUser> userManager, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }

        public async Task<Contract> CreateContractAsync(int? supplierId, ContractCreateDto request)
        {
            if (supplierId is null)
            {
                throw new ArgumentNullException(nameof(supplierId));
            }
            if (request is null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var supplier = await _context.Suppliers.FirstOrDefaultAsync(s => s.Id == supplierId);
            if (supplier == null)
            {
                throw new NotFoundDataException();
            }

            var fileData = await ConvertToFileData.ConvertFromIFormFile(request.TermsAndConditions!);

            var currentUser = await _userManager.GetUserAsync(_contextAccessor.HttpContext!.User);

            fileData.Uploader = currentUser;
            _context.FilesData.Add(fileData);
            await _context.SaveChangesAsync();

            var contract = new Contract
            {
                Supplier = supplier,
                Customer = currentUser,
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                ContractValue = request.ContractValue,
                TermsAndConditions = fileData,
            };

            return contract;
        }
    }
}
