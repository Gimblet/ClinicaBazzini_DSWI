using ClinicaWebApp.Models.Cita;
using ClinicaWebApp.Models.Pago;
using ClinicaWebApp.Models.Usuario.Medico;
using ClinicaWebApp.Models.Usuario.Paciente;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;

namespace ClinicaWebApp.Controllers;

public class CitaController : Controller
{
    private readonly Uri _baseUri = new("http://localhost:5000/api");
    private readonly HttpClient _httpClient;

    public CitaController()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = _baseUri;
    }

    public List<Cita> ArregloClitas()
    {
        List<Cita> aCitas = new List<Cita>();
        HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/Cita/listaCita").Result;
        var data = response.Content.ReadAsStringAsync().Result;
        aCitas = JsonConvert.DeserializeObject<List<Cita>>(data);
        return aCitas;
    }

    public List<Medico> listadoMedico()
    {
        List<Medico> aMedicos = new List<Medico>();
        HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/Medico/listaMedicos").Result;
        var data = response.Content.ReadAsStringAsync().Result;
        aMedicos = JsonConvert.DeserializeObject<List<Medico>>(data);
        return aMedicos;
    }

    public List<Paciente> listadoPaciente()
    {
        List<Paciente> aPacientes = new List<Paciente>();
        HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/Paciente/listaPaciente").Result;
        var data = response.Content.ReadAsStringAsync().Result;
        aPacientes = JsonConvert.DeserializeObject<List<Paciente>>(data);
        return aPacientes;
    }



    public List<Cita> listarCitasPorFecha(int dia, int mes, int año)
    {
        List<Cita> citas = new List<Cita>();
        HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + $"/Cita/listaCitaPorFecha?dia={dia}&mes={mes}&año={año}").Result;

        var data = response.Content.ReadAsStringAsync().Result;
        citas = JsonConvert.DeserializeObject<List<Cita>>(data);
        

        return citas;
    }



    public IActionResult ListadoCitas(int? dia, int?mes, int? año)
    {
        List<Cita> citas = ArregloClitas();
        if (dia.HasValue && mes.HasValue && año.HasValue)
        {   
            citas = listarCitasPorFecha(dia.Value, mes.Value, año.Value);
        }
        
        ViewBag.Dia = dia;
        ViewBag.Mes = mes;
        ViewBag.Año = año;
        return View(citas);
    }
    [HttpGet]
    public IActionResult nuevaCita() 
    {
        ViewBag.medicos = new SelectList(listadoMedico(), "IdMedico","NombreUsuario");
        ViewBag.pacientes = new SelectList(listadoPaciente(), "IdPaciente", "NombreUsuario");
        return View(new CitaO());
    }

    [HttpPost]
    public async Task<IActionResult> nuevaCita(CitaO obj)
    {
        if (!ModelState.IsValid)
         {
            ViewBag.medicos = new SelectList(listadoMedico(), "IdMedico", "NombreUsuario");
            ViewBag.pacientes = new SelectList(listadoPaciente(), "IdPaciente", "NombreUsuario");
            return View(obj);
        }
        var json = JsonConvert.SerializeObject(obj);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var responseC = await _httpClient.PostAsync("/api/Cita/agregaCita", content);
        if (responseC.IsSuccessStatusCode)
        {
            ViewBag.mensaje = "Cita registrado correctamente..!!!";
        }

        ViewBag.medicos = new SelectList(listadoMedico(), "IdMedico", "NombreUsuario");
        ViewBag.pacientes = new SelectList(listadoPaciente(), "IdPaciente", "NombreUsuario");
        return RedirectToAction("ListadoCitas");
    }

    [HttpGet]
    public async Task<IActionResult> actualizarCita(int id)
    {
        var response = await
             _httpClient.GetAsync(_httpClient.BaseAddress + "/Cita/buscarCita/" + id);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var objC = JsonConvert.DeserializeObject<CitaO>(content);
            ViewBag.medicos = new SelectList(listadoMedico(), "IdMedico", "NombreUsuario");
            ViewBag.pacientes = new SelectList(listadoPaciente(), "IdPaciente", "NombreUsuario");
            return View(objC);
        }
        else
        {
            ViewBag.mensaje = "No Hay Cita";

        }
        ViewBag.medicos = new SelectList(listadoMedico(), "IdMedico", "NombreUsuario");
        ViewBag.pacientes = new SelectList(listadoPaciente(), "IdPaciente", "NombreUsuario");
        return View();
    }


    [HttpPost]
    public async Task<IActionResult> actualizarCita(int id,CitaO obj)
    {
        var json = JsonConvert.SerializeObject(obj);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var response = await
              _httpClient.PutAsync("/api/Cita/actualizaCita?id={ id }", content);
        if (response.IsSuccessStatusCode)
        {
            ViewBag.medicos = new SelectList(listadoMedico(), "IdMedico", "NombreUsuario");
            ViewBag.pacientes = new SelectList(listadoPaciente(), "IdPaciente", "NombreUsuario");
            ViewBag.mensaje = "Cita actualizada correctamente";
        }

        ViewBag.medicos = new SelectList(listadoMedico(), "IdMedico", "NombreUsuario");
        ViewBag.pacientes = new SelectList(listadoPaciente(), "IdPaciente", "NombreUsuario");
        return RedirectToAction("ListadoCitas");
    }
    [HttpPost]
    public async Task<IActionResult> eliminarCita(int id)
    {
        var response = await _httpClient.DeleteAsync($"/api/Cita/eliminarCita/{id}");

        if (response.IsSuccessStatusCode)
        {
            TempData["mensaje"] = "Cita eliminada correctamente.";
        }
        else
        {
            TempData["mensaje"] = "Error al eliminar la cita.";
        }

        return RedirectToAction("ListadoCitas");
    }



    public IActionResult Index()
    {
        return View();
    }
}