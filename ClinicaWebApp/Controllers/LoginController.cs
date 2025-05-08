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

    //Si el usuario existe devuelve el rol(Paciente, Medico, Recepcionista) caso contrario devuelve denied
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


    [HttpGet]
    public IActionResult Index()
    {
        HttpContext.Session.Clear();
        return View();
    }

    [HttpPost]
    public IActionResult Index(string uid, string pwd)
    {
        string respuesta = IniciarSesion(Uri.EscapeDataString(uid), Uri.EscapeDataString(pwd));

        if (respuesta.Equals("denied"))
        {
            ViewBag.correo = uid;
            ViewBag.Mensaje = "Usuario o contraseña incorrectos.";
            return RedirectToAction("Index", "Login");
        }
        string token = ObtenerToken(Uri.EscapeDataString(uid));
        HttpContext.Session.SetString("token", ObtenerToken(Uri.EscapeDataString(uid)));
        return RedirectToAction("Index", respuesta);
    }
}