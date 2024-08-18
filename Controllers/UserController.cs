using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Supplier_SRM_MVC.Dto;
using Supplier_SRM_MVC.Exceptions;
using Supplier_SRM_MVC.Interfaces;

namespace Supplier_SRM_MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(AppUserRegisterDto request)
        {
            try
            {
                var result = await _userService.CreateUserAsync(request);
                return RedirectToAction("Index", "Home");
            }
            catch (ArgumentNullException)
            {
                return BadRequest();
            }
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AppUserLoginDto request)
        {
            var result = await _userService.LoginAsync(request);

            if (result != null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return BadRequest();
            }
        }


        // TODO - СОЗДАТЬ ВЬЮШКУ ДЛЯ ПРОФИЛИ
        //[HttpGet("profile")]
        //public async Task<IActionResult> GetUserDataAsync()
        //{
        //    try
        //    {
        //        var result = await _userService.GetUserDataAsync();
        //        return Ok(result);
        //    }
        //    catch (ArgumentNullException ex)
        //    {
        //        return BadRequest(ex.Message);
        //    }
        //    catch (NotFoundDataException)
        //    {
        //        return NotFound();
        //    }
        //}
    }
}
