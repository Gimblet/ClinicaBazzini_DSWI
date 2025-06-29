﻿using ClinicaWebApp.Models.Usuario.Paciente;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Rotativa.AspNetCore;

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
            HttpResponseMessage response =
                await _httpClient.GetAsync(_httpClient.BaseAddress + $"/Paciente/listaCitaPorPaciente/{ide_usr}");

            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                aCitaPaciente = JsonConvert.DeserializeObject<List<CitaPaciente>>(data);
            }

            return aCitaPaciente;
        }

        public Paciente ObtenerPacientePorId(long id)
        {
            Paciente pago = new Paciente();
            HttpResponseMessage response =
                _httpClient.GetAsync(_httpClient.BaseAddress + $"/Paciente/buscarPaciente/{id}").Result;
            var data = response.Content.ReadAsStringAsync().Result;
            pago = JsonConvert.DeserializeObject<Paciente>(data);
            return pago;
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
            if (id == 0 || id == null)
            {
                return Content("ID del paciente no recibido");
            }

            return View(ObtenerPacientePorId(id));
        }

        public IActionResult DetallePacientePDF(long id)
        {
            String hoy = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            return new ViewAsPdf("DetallePacientePDF", ObtenerPacientePorId(id))
            {
                FileName = $"DetallePaciente-{hoy}.pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A5
            };
        }

        public async Task<IActionResult> Index(long id)
        {
            HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress + $"/Cita/citasPendientesXPaciente/{id}");

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