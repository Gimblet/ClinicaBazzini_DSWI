﻿using ClinicaWebApp.Models.Usuario.Medico;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using ClinicaWebApp.Models.Usuario;
using ClinicaWebApp.Models.Usuario.Paciente;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rotativa.AspNetCore;
using Rotativa.AspNetCore.Options;

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

        [HttpGet]
        public IActionResult NuevoMedico()
        {
            ViewBag.especialidad = new SelectList(ArregloEspecialidad(), "ide_esp", "nom_esp");
            ViewBag.documentos = new SelectList(ArregloTipoDocumentos(), "ide_doc", "nom_doc");
            return View();
        }

        [HttpPost]
        public IActionResult NuevoMedico(MedicoO medico)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.especialidad = new SelectList(ArregloEspecialidad(), "ide_esp", "nom_esp");
                ViewBag.documentos = new SelectList(ArregloTipoDocumentos(), "ide_doc", "nom_doc");
                return View(medico);
            }

            //TODO: Agregar Mensaje de Validación
            ViewBag.respuesta = AgregarMedico(medico);
            return RedirectToAction("listarMedicos");
        }

        public IActionResult listarMedicos()
        {
            return View(ArregloMedicos());
        }

        public IActionResult listarMedicosPDF()
        {
            String hoy = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            return new ViewAsPdf("listarMedicosPDF", ArregloMedicos())
            {
                FileName = $"ListadoMedicos-{hoy}.pdf",
                PageOrientation = Orientation.Portrait,
                PageSize = Size.A4
            };
        }

        [HttpGet]
        public IActionResult NuevoPaciente()
        {
            ViewBag.documentos = new SelectList(ArregloTipoDocumentos(), "ide_doc", "nom_doc");
            return View();
        }

        [HttpPost]
        public IActionResult NuevoPaciente(PacienteO paciente)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.documentos = new SelectList(ArregloTipoDocumentos(), "ide_doc", "nom_doc");
                return View(paciente);
            }

            //TODO: Agregar Mensaje de Validación
            ViewBag.respuesta = AgregarPaciente(paciente);
            return RedirectToAction("listarPacientes");
        }

        public IActionResult listarPacientes()
        {
            return View(ArregloPacientes());
        }

        public IActionResult listarPacientesPDF()
        {
            String hoy = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            return new ViewAsPdf("listarPacientesPDF", ArregloPacientes())
            {
                FileName = $"ListadoPacientes-{hoy}.pdf",
                PageOrientation = Orientation.Portrait,
                PageSize = Size.A4
            };
        }

        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress + "/Cita/totalCitasXDia");

            var citas = 0;

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                citas = JsonConvert.DeserializeObject<int>(data);
            }

            ViewBag.citas = citas;
            return View();
        }
    }
}
