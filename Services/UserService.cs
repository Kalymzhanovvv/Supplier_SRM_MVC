using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Supplier_SRM_MVC.Dto;
using Supplier_SRM_MVC.Entities;
using Supplier_SRM_MVC.Exceptions;
using Supplier_SRM_MVC.Interfaces;
using Supplier_SRM_MVC.Utilities;

namespace Supplier_SRM_MVC.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly string _adminEmail = "ADMIN@GMAIL.COM";

        public UserService(ApplicationDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _contextAccessor = contextAccessor;
        }


        public async Task<AppUser> CreateUserAsync(AppUserRegisterDto request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var searchEmail = await _context.AppUsers.FirstOrDefaultAsync(x => x.Email == request.Email);
            if (searchEmail != null)
            {
                throw new Exception($"User with email {request.Email} not found");
            }

            var newAppUser = new AppUser
            {
                Name = request.Name,
                UserName = request.Email,
                Email = request.Email,
            };
            var result = await _userManager.CreateAsync(newAppUser, request!.Password!);

            if (result.Succeeded)
            {
                // присвоил роль
                if (!await _userManager.IsInRoleAsync(newAppUser, Roles.Admin) && newAppUser.NormalizedEmail == _adminEmail)
                {
                    await _userManager.AddToRoleAsync(newAppUser, Roles.Admin);
                    _context.AppUsers.Update(newAppUser);
                    await _context.SaveChangesAsync();
                }
                return newAppUser;
            }
            else
            {
                return null!;
            }
        }

        public async Task<AppUser> LoginAsync(AppUserLoginDto request)
        {
            var result = await _signInManager.PasswordSignInAsync(request.Email!, request.Password!, false, false);

            if (result.Succeeded)
            {
                var currentUser = await _context.AppUsers.FirstOrDefaultAsync(x => x.Email == request.Email);
                return currentUser!;
            }
            else
            {
                return default!;
            }
        }

        public async Task<AppUser> GetUserDataAsync()
        {
            //if (userId == null)
            //{
            //    throw new ArgumentNullException(nameof(userId));
            //}

            //var currentUser = await _userManager.FindByIdAsync(userId);
            //if (currentUser == null)
            //{
            //    throw new NotFoundDataException();
            //}

            var currentUser = await _userManager.GetUserAsync(_contextAccessor.HttpContext!.User);
            if (currentUser == null)
            {
                throw new NotFoundDataException();
            }

            return currentUser;
        }
    }
}
