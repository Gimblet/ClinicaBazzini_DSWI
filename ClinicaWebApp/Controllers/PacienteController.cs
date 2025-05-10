using ClinicaWebApp.Models.Usuario.Paciente;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClinicaWebApp.Controllers
{
    public class PacienteController : Controller
    {

        private readonly Uri _baseUri = new("http://localhost:5000/api");
        private readonly HttpClient _httpClient;

        public PacienteController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = _baseUri;
        }

        public async Task<List<CitaPaciente>> aCitaPaciente(long ide_usr)
        {
            List<CitaPaciente> aCitaPaciente = new();
            HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress + $"/Paciente/listaCitaPorPaciente/{ide_usr}");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                aCitaPaciente = JsonConvert.DeserializeObject<List<CitaPaciente>>(data);
            }

            return aCitaPaciente;
        }

        public async Task<IActionResult> listaCitaPorPaciente(long ide_usr)
        {
            if (ide_usr == 0)
            {
                return Content("ID del paciente no recibido o inválido");
            }

            var citas = await aCitaPaciente(ide_usr);
            return View(citas);
        }

        public IActionResult DetallePaciente(long id)
        {
            return View();
        }
        public IActionResult Index()
        {
            return View();
        }


    }
}
