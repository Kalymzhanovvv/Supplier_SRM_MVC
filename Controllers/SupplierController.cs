using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Supplier_SRM_MVC.Dto;
using Supplier_SRM_MVC.Entities;
using Supplier_SRM_MVC.Exceptions;
using Supplier_SRM_MVC.Interfaces;
using Supplier_SRM_MVC.Utilities;

namespace Supplier_SRM_MVC.Controllers
{
    public class SupplierController : Controller
    {
        private readonly ISupplierService _supplierService;

        public SupplierController(ISupplierService supplierService)
        {
            _supplierService = supplierService;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SupplierCreateDto request)
        {
            try
            {
                var result = await _supplierService.RegisterSupplierAsync(request);
                return RedirectToAction("Index", "Home");
            }
            catch (DuplicateWaitObjectException ex)
            {
                return Conflict(ex.Message);
            }
        }

        public async Task<IActionResult> Offers()
        {
            try
            {
                var result = await _supplierService.GetOffers();
                return View(result);
            }
            catch (NotFoundDataException)
            {
                return NotFound();
            }
        }

        // TODO - СОЗДАТЬ ВЬЮШКУ ДЛЯ ПРОФИЛИ
        //[Authorize(Roles = "Supplier")]
        //[HttpGet("profile")]
        //public async Task<IActionResult> GetSupplierAsync()
        //{
        //    try
        //    {
        //        var result = await _supplierService.GetSupplierDataAsync();
        //        return Ok(result);
        //    }
        //    catch (NotFoundDataException)
        //    {
        //        return NotFound();
        //    }
        //}

    }
}
