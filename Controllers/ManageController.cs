using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Supplier_SRM_MVC.Dto;
using Supplier_SRM_MVC.Interfaces;
using Supplier_SRM_MVC.Utilities;

namespace Supplier_SRM_MVC.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ManageController : Controller
    {
        private readonly IManageService _manageService;

        public ManageController(IManageService manageService)
        {
            _manageService = manageService;
        }


        // получение списка всех поставщиков ждущие подтверждение
        public async Task<IActionResult> GetWaiting()
        {
            return Ok(await _manageService.GetPendingApprovalSuppliersAsync());
        }


        // подтверждение заявки юзера на позицию поставщика
        [HttpPost]
        public async Task<IActionResult> ConfirmSupplier(int? id, string? confirmBtnValue)
        {
            try
            {
                var result = await _manageService.ConfirmSupplier(confirmBtnValue, id);
                return RedirectToAction("GetWaiting");
            }
            catch(Exception ex)
            {
                return NotFound(ex.Message);
            }

        }
    }
}
