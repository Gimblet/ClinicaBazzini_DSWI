using ClinicaWebApp.Models.Cita;
using ClinicaWebApp.Models.Usuario.Medico;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Numerics;
using System.Text;

namespace ClinicaWebApp.Controllers
{
    public class RecepcionistaController : Controller
    {
        private readonly Uri _baseUri = new("http://localhost:5000/api");
        private readonly HttpClient _httpClient;

        public RecepcionistaController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = _baseUri;
        }

        public List<Medico> ArregloMedicos()
        {
            List<Medico> aMedicos = new List<Medico>();
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/Medico/listaMedicos").Result;
            var data = response.Content.ReadAsStringAsync().Result;
            aMedicos = JsonConvert.DeserializeObject<List<Medico>>(data);
            return aMedicos;
        }

        public String AgregarMedico(MedicoO medico)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(medico), encoding: UTF8Encoding.UTF8, "application/json");
            HttpResponseMessage response = _httpClient.PostAsync(_httpClient.BaseAddress + "/Medico/nuevoMedico", content).Result;
            return response.Content.ReadAsStringAsync().Result;
        }

        public IActionResult NuevoMedico(MedicoO medico)
        {
            if (!ModelState.IsValid)
            {
                return View(medico);
            }
            string respuesta = AgregarMedico(medico);
            return View();
        }

        public IActionResult listarMedicos()
        {
            return View(ArregloMedicos());
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
