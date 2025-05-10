using ClinicaWebApp.Models.Usuario.Medico;

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClinicaWebApp.Controllers
{
    public class MedicoController : Controller
    {
        private readonly Uri _baseUri = new("http://localhost:5000/api/");
        private readonly HttpClient _httpClient;

        public MedicoController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = _baseUri;

        }

        public async Task<List<CitaMedico>> ArregloCitaMedico(long ide_usr)
        {
            List<CitaMedico> aCitaMedico = new();
            HttpResponseMessage response = await _httpClient.GetAsync($"Medico/listaCitaPorMedicos/{ide_usr}");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                aCitaMedico = JsonConvert.DeserializeObject<List<CitaMedico>>(data);
            }

            return aCitaMedico;
        }


        public async Task<IActionResult> listaCitaPorMedicos(long ide_usr)
        {
            if (ide_usr == 0)
            {
                return Content("ID del médico no recibido o inválido");
            }

            var citas = await ArregloCitaMedico(ide_usr);
            return View(citas);
        }


        public async Task<IActionResult> Index()
        {
            var medicoId = HttpContext.Session.GetInt32("MedicoId");
            if (medicoId == null || medicoId == 0)
            {
                return RedirectToAction("Login", "Account"); 
            }

            // Obtener las estadísticas
            MedicoStats stats = new MedicoStats();
            HttpResponseMessage response = await _httpClient.GetAsync($"Medico/estadisticasMedico/{medicoId}");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                stats = JsonConvert.DeserializeObject<MedicoStats>(data);
            }

            var medico = new Medico();
            response = await _httpClient.GetAsync($"Medico/buscarMedico/{medicoId}");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                medico = JsonConvert.DeserializeObject<Medico>(data);
            }

            ViewBag.Stats = stats;
            ViewBag.Medico = medico;

            return View();
        }
    }
}
