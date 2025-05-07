using ClinicaWebApp.Models.Usuario.Medico;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using ClinicaWebApp.Models.Usuario;
using ClinicaWebApp.Models.Usuario.Paciente;
using Microsoft.AspNetCore.Mvc.Rendering;

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

        public List<Paciente> ArregloPacientes()
        {
            List<Paciente> aPacientes = new List<Paciente>();
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/Paciente/listaPacientes").Result;
            var data = response.Content.ReadAsStringAsync().Result;
            aPacientes = JsonConvert.DeserializeObject<List<Paciente>>(data);
            return aPacientes;
        }

        public List<Especialidad> ArregloEspecialidad()
        {
            List<Especialidad> aEspecialidad = new List<Especialidad>();
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/Medico/listarEspecialidad").Result;
            var data = response.Content.ReadAsStringAsync().Result;
            aEspecialidad = JsonConvert.DeserializeObject<List<Especialidad>>(data);
            return aEspecialidad;
        }

        public List<UserDoc> ArregloTipoDocumentos()
        {
            List<UserDoc> aTDocumentos = new List<UserDoc>();
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/Usuario/ListarDocumentos").Result;
            var data = response.Content.ReadAsStringAsync().Result;
            aTDocumentos = JsonConvert.DeserializeObject<List<UserDoc>>(data);
            return aTDocumentos;
        }

        public String AgregarMedico(MedicoO medico)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(medico), encoding: UTF8Encoding.UTF8, "application/json");
            HttpResponseMessage response = _httpClient.PostAsync(_httpClient.BaseAddress + "/Medico/nuevoMedico", content).Result;
            return response.Content.ReadAsStringAsync().Result;
        }

        public String AgregarPaciente(PacienteO paciente)
        {
            StringContent content = new StringContent(JsonConvert.SerializeObject(paciente), encoding: UTF8Encoding.UTF8, "application/json");
            HttpResponseMessage response = _httpClient.PostAsync(_httpClient.BaseAddress + "/Paciente/nuevoPaciente", content).Result;
            return response.Content.ReadAsStringAsync().Result;
        }

        public IActionResult NuevoMedico(MedicoO medico)
        {
            if (!ModelState.IsValid)
            {
                return View(medico);
            }

            ViewBag.especialidad = new SelectList(ArregloEspecialidad(), "ide_esp", "nom_esp");
            ViewBag.documentos = new SelectList(ArregloTipoDocumentos(), "ide_doc", "nom_doc");
            //TODO: Agregar Mensaje de Validación
            ViewBag.respuesta = AgregarMedico(medico);
            return View();
        }

        public IActionResult listarMedicos()
        {
            return View(ArregloMedicos());
        }

        public IActionResult NuevoPaciente(PacienteO paciente)
        {
            if (!ModelState.IsValid)
            {
                return View(paciente);
            }

            //TODO: Agregar Mensaje de Validación
            ViewBag.respuesta = AgregarPaciente(paciente);
            return View();
        }

        public IActionResult listarPacientes()
        {
            return View(ArregloPacientes());
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
