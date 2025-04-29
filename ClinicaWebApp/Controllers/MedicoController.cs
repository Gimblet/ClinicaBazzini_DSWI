using Microsoft.AspNetCore.Mvc;

namespace ClinicaWebApp.Controllers
{
    public class MedicoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
