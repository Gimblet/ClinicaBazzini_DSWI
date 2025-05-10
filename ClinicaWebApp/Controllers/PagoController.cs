using ClinicaWebApp.Models.Pago;
using ClinicaWebApp.Models.Usuario.Paciente;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using NuGet.Protocol;
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

        public List<Pago> listadoPagosGeneral()
        {
            List<Pago> aPagos = new List<Pago>();
            HttpResponseMessage response =
                _httpClient.GetAsync(_httpClient.BaseAddress + "/Pago/ListarPagosGeneral").Result;
            var data = response.Content.ReadAsStringAsync().Result;
            aPagos = JsonConvert.DeserializeObject<List<Pago>>(data);
            return aPagos;
        }

        public Pago ObtenerPagoPorId(long id)
        {
            Pago pago = new Pago();
            HttpResponseMessage response =
                _httpClient.GetAsync(_httpClient.BaseAddress + $"/Pago/ObtenerPagoPorId/{id}").Result;
            var data = response.Content.ReadAsStringAsync().Result;
            pago = JsonConvert.DeserializeObject<Pago>(data);
            return pago;
        }

        public List<Pago> listadoPagosPorPaciente(long token)
        {
            List<Pago> aPagos = new List<Pago>();
            HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress +
                                                                "/Pago/ListarPagosPorPaciente" +
                                                                $"/{token}").Result;
            var data = response.Content.ReadAsStringAsync().Result;
            aPagos = JsonConvert.DeserializeObject<List<Pago>>(data);
            return aPagos;
        }

        public List<PayOpts> ListadoPayOpts()
        {
            List<PayOpts> aPayOpts = new List<PayOpts>();
            HttpResponseMessage response =
                _httpClient.GetAsync(_httpClient.BaseAddress + "/Pago/ObtenerTiposDePago").Result;
            var data = response.Content.ReadAsStringAsync().Result;
            aPayOpts = JsonConvert.DeserializeObject<List<PayOpts>>(data);
            return aPayOpts;
        }

        public List<Paciente> listadoPaciente()
        {
            List<Paciente> aPacientes = new List<Paciente>();
            HttpResponseMessage response =
                _httpClient.GetAsync(_httpClient.BaseAddress + "/Paciente/listaPacientes").Result;
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
            var responseC = await _httpClient.PostAsync(_httpClient.BaseAddress + $"/Pago/AgregarPago/{obj.IdPaciente}",
                content);
            if (responseC.IsSuccessStatusCode)
            {
                ViewBag.mensaje = "Pago registrado correctamente..!!!";
                Console.WriteLine("Pago " + obj.ToJson());
            }

            ViewBag.tipoPagos = new SelectList(ListadoPayOpts(), "ide_pay", "nom_pay");
            ViewBag.pacientes = new SelectList(listadoPaciente(), "IdPaciente", "NombreUsuario");
            
            var idPagoStr = await responseC.Content.ReadAsStringAsync();
            long IdPago = long.Parse(idPagoStr);


            return RedirectToAction("nuevaCita", "Cita", new { PagoId = IdPago });
        }

        public IActionResult PagosRecepcionista()
        {
            return View(listadoPagosGeneral());
        }

        public IActionResult PagosPaciente()
        {
            long token = long.Parse(HttpContext.Session.GetString("token"));
            return View(listadoPagosPorPaciente(token));
        }

        public IActionResult DetallePago(long id)
        {
            if (id == null || id == 0)
            {
                return Content("Error al intentar obtener el pago");
            }
            return View(ObtenerPagoPorId(id));
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}