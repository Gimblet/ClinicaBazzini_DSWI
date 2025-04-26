using ClinicaAPI.Models.Usuario.Paciente;
using ClinicaAPI.Repository.DAO;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PacienteController : Controller
{
    [HttpGet("listaPaciente")]
    public async Task<ActionResult<List<Paciente>>> ListaPacientes()
    {
        var lista = await Task.Run(()
            => new PacienteDAO().ListarPacientes());
        return Ok(lista);
    }

    [HttpPost("guardarPaciente")]
    public async Task<ActionResult<string>> GuardarPaciente(PacienteO paciente)
    {
        var mensaje = await Task.Run(()
            => new PacienteDAO().GuardarPacienteO(paciente));
        return Ok(mensaje);
    }
}