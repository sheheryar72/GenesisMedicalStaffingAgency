using Microsoft.AspNetCore.Mvc;

namespace GenesisMedicalStaffingAgency.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
