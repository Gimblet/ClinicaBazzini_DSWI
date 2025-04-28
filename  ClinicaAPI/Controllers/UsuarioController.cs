using ClinicaAPI.Models.Usuario;
using ClinicaAPI.Repository.DAO;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuarioController : Controller
{
    [HttpGet("[action]")]
    public async Task<ActionResult<string>> VerificarLogin(string uid, string pwd)
    {
        var resultado = await Task.Run(()
            => new UsuarioDAO().verificarLogin(uid, pwd));
        return Ok(resultado);
    }
    
    [HttpGet("[action]")]
    public async Task<ActionResult<string>> ObtenerToken(string correo)
    {
        var resultado = await Task.Run(()
            => new UsuarioDAO().obtenerIdUsuario(correo));
        return Ok(resultado);
    }

    // [HttpPost("guardarPaciente")]
    // public async Task<ActionResult<string>> GuardarPaciente(UsuarioO usuario)
    // {
    //     var mensaje = await Task.Run(()
    //         => new PacienteDAO().GuardarPacienteO(usuario));
    //     return Ok(mensaje);
    // }
}