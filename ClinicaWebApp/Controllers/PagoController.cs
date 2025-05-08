using ClinicaWebApp.Models.Pago;
using ClinicaWebApp.Models.Usuario.Paciente;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;

namespace ClinicaWebApp.Controllers
{
    public class PagoController : Controller
    {
        private readonly Uri _baseUri = new("http://localhost:5000/api");
        private readonly HttpClient _httpClient;

        public PagoController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = _baseUri;
        }

        public List<PayOpts> ListadoPayOpts()
        {
            List<PayOpts> aPayOpts = new List<PayOpts>();
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/Pago/ObtenerTiposDePago").Result;
            var data = response.Content.ReadAsStringAsync().Result;
            aPayOpts = JsonConvert.DeserializeObject<List<PayOpts>>(data);
            return aPayOpts;
        }
        public List<Paciente> listadoPaciente()
        {
            List<Paciente> aPacientes = new List<Paciente>();
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/Paciente/listaPacientes").Result;
            var data = response.Content.ReadAsStringAsync().Result;
            aPacientes = JsonConvert.DeserializeObject<List<Paciente>>(data);
            return aPacientes;
        }

        public IActionResult Crear()
        { 
            ViewBag.tipoPagos = new SelectList(ListadoPayOpts(), "ide_pay", "nom_pay");
            ViewBag.pacientes = new SelectList(listadoPaciente(), "IdPaciente", "NombreUsuario");
            return View(new PagoO());
        }
        [HttpPost]
        public async Task<IActionResult> Crear(PagoO obj)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.tipoPagos = new SelectList(ListadoPayOpts(), "ide_pay", "nom_pay");
                ViewBag.pacientes = new SelectList(listadoPaciente(), "IdPaciente", "NombreUsuario");
                return View(obj);
            }

            obj.IdPaciente = int.Parse(HttpContext.Session.GetString("token"));
            var json = JsonConvert.SerializeObject(obj);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var responseC = await _httpClient.PostAsync(_httpClient.BaseAddress + $"/Pago/AgregarPago/{obj.IdPaciente}", content);
            if (responseC.IsSuccessStatusCode)
            {
                ViewBag.mensaje = "Cita registrado correctamente..!!!";
            }

            ViewBag.tipoPagos = new SelectList(ListadoPayOpts(), "ide_pay", "nom_pay");
            ViewBag.pacientes = new SelectList(listadoPaciente(), "IdPaciente", "NombreUsuario");
            return RedirectToAction("nuevaCita", "Cita", new { PagoId = obj.IdPago });

        }




        public IActionResult Index()
        {

            return View();
        }
    }
}
