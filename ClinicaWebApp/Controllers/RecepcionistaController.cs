using Microsoft.AspNetCore.Mvc;

namespace ClinicaWebApp.Controllers
{
    public class RecepcionistaController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
