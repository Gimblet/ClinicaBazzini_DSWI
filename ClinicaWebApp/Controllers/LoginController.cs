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
        var response = _httpClient.GetAsync(_httpClient.BaseAddress +
                                            "/Usuario/VerificarLogin" +
                                            "?uid=" + uid +
                                            "&pwd=" + pwd).Result;
        return response.Content.ReadAsStringAsync().Result;
    }
    
    public string ObtenerToken(string uid)
    {
        var response = _httpClient.GetAsync(_httpClient.BaseAddress +
                                            "/Usuario/ObtenerToken" +
                                            "?correo=" + uid).Result;
        return response.Content.ReadAsStringAsync().Result;
    }

    public IActionResult Index(string uid, string pwd)
    {
        switch (IniciarSesion(Uri.EscapeDataString(uid), Uri.EscapeDataString(pwd)))
        {
            case "secretaria": 
                HttpContext.Session.SetString("token", pwd);
                return RedirectToAction("Index", "Recepcionista");
            case "medico": return RedirectToAction("Index", "Medico");
            case "paciente": return RedirectToAction("Index", "Paciente");
            default:
                ViewBag.correo = uid;
                return RedirectToAction("Index", "Login");
        }
    }
}