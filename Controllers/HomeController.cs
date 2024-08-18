using Microsoft.AspNetCore.Mvc;

namespace Supplier_SRM_MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
