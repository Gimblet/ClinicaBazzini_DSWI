using Microsoft.AspNetCore.Mvc;

namespace ClinicaWebApp.Controllers
{
    public class PacienteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
