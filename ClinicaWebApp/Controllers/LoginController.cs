using Microsoft.AspNetCore.Mvc;

namespace ClinicaWebApp.Controllers;

public class LoginController : Controller
{
    private readonly Uri _baseUri = new("http://localhost:5000/api");
    private readonly HttpClient _httpClient;

    public LoginController()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = _baseUri;
    }

    public string IniciarSesion(string uid, string pwd)
    {
        uid = Uri.EscapeDataString(uid);
        pwd = Uri.EscapeDataString(pwd);
        var response = _httpClient.GetAsync(_httpClient.BaseAddress +
                                            "/Usuario/VerificarLogin" +
                                            "?uid=" + uid +
                                            "&pwd=" + pwd).Result;
        return response.Content.ReadAsStringAsync().Result;
    }

    // GET
    public IActionResult Index(string uid, string pwd)
    {
        switch (IniciarSesion("joseph@gmail.com", "joseph1234"))
        {
            case "secretaria": return RedirectToAction("Index", "Recepcionista");
            case "medico": return RedirectToAction("Index", "Medico");
            case "paciente": return RedirectToAction("Index", "Paciente");
            default: return RedirectToAction("Index", "Login");
        }
    }
}