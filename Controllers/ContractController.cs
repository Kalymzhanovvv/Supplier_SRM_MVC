using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Supplier_SRM_MVC.Dto;
using Supplier_SRM_MVC.Exceptions;
using Supplier_SRM_MVC.Interfaces;

namespace Supplier_SRM_MVC.Controllers
{
    [Authorize]
    public class ContractController : Controller
    {
        private readonly IContractService _contractService;

        public ContractController(IContractService contractService)
        {
            _contractService = contractService;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(int? supplierId, ContractCreateDto request)
        {
            try
            {
                var result = await _contractService.CreateContractAsync(supplierId, request);
                return RedirectToAction("Index", "Home");
            }
            catch (ArgumentNullException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundDataException)
            {
                return NotFound();
            }
        }
    }
}
