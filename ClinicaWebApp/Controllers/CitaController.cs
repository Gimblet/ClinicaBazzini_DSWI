using ClinicaWebApp.Models.Cita;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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

    public IActionResult ListadoCitas()
    {
        return View(ArregloClitas());
    }
}