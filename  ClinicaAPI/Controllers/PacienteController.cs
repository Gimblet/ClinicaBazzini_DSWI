using ClinicaAPI.Models.Usuario;
using ClinicaAPI.Models.Usuario.Medico;
using ClinicaAPI.Models.Usuario.Paciente;
using ClinicaAPI.Repository.DAO;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PacienteController : Controller
{
    [HttpGet("listaPacientes")]
    public async Task<ActionResult<List<Paciente>>> listaPacientes()
    {
        var lista = await Task.Run(()
            => new PacienteDAO().ListarPacientes());
        return Ok(lista);
    }

    [HttpGet("listaPacientesBackend")]
    public async Task<ActionResult<List<PacienteO>>> listaPacientesBackend()
    {
        var lista = await Task.Run(()
            => new PacienteDAO().ListarPacientesO());
        return Ok(lista);
    }

    [HttpPost("nuevoPaciente")]
    public async Task<ActionResult<string>> guardarPaciente(PacienteO P)
    {
        var mensaje = await Task.Run(()
            => new PacienteDAO().GuardarPacienteO(P));
        return Ok(mensaje);
    }

    [HttpGet("buscarPaciente/{id}")]
    public async Task<ActionResult<PacienteO>> buscarPaciente(long id)
    {
        var lista = await Task.Run(()
            => new PacienteDAO().buscarPacientePorID(id));
        return Ok(lista);
    }

    [HttpPut("actualizarPaciente")]
    public async Task<ActionResult<string>> actualizarMedico(PacienteO P)
    {
        var mensaje = await Task.Run(()
            => new PacienteDAO().actualizarPaciente(P));
        return Ok(mensaje);
    }

    [HttpDelete("eliminarPaciente/{id}")]
    public async Task<ActionResult> eliminarPaciente(long id)
    {
        var lista = await Task.Run(()
            => new PacienteDAO().eliminarPaciente(id));
        return Ok(lista);
    }
    [HttpGet("listaCitaPorPaciente/{ide_usr}")]
    public ActionResult<List<CitaPaciente>> listaCitaPorPaciente(long ide_usr)
    {
        var lista = new PacienteDAO().listarCitaPaciente(ide_usr);
        return Ok(lista);
    }
}