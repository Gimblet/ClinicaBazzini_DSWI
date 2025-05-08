using ClinicaAPI.Models.Cita;
using ClinicaAPI.Repository.DAO;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CitaController : Controller
{
    [HttpGet("listaCita")]
    public async Task<ActionResult<List<Cita>>> ListaCita()
    {
        var lista = await Task.Run(()
            => new CitaDAO().listarCitas());
        return Ok(lista);
    }
    [HttpGet("listaCitaO")]
    public async Task<ActionResult<List<Cita>>> ListaCitaO() 
    {
        var lista = await Task.Run(()
            => new CitaDAO().listarCitasO());
        return Ok(lista);

    }
    [HttpPost("agregaCita")]
    public async Task<ActionResult<string>> agregaCita(CitaO obj)
    {
        var mensaje = await Task.Run(() => new CitaDAO().agregarCita(obj));
        return Ok(mensaje);
    }
    
    [HttpPut("actualizaCita")]
    public async Task<ActionResult<string>> actualizarCita(CitaO obj)
    {
        var mensaje = await Task.Run(() => new CitaDAO().modificarCita(obj));
        return Ok(mensaje);
    }
    [HttpDelete("eliminarCita/{id}")]
    public async Task<ActionResult> eliminarCita(long id)
    {
        await Task.Run(() => new CitaDAO().eliminarCita(id));
        return Ok();
    }
    [HttpGet("buscarCita/{id}")]
    public async Task<ActionResult> buscarCita(long id)
    {
        var lista = await Task.Run(() => new CitaDAO().buscarCita(id));
        return Ok(lista);
    }

    [HttpGet("listaCitaPorFecha")]
    public async Task<ActionResult<List<Cita>>> ListaCitaPorFecha(int dia,int mes,int año) {
        var lista = await Task.Run(() => new CitaDAO().listarCitaPorFecha(dia, mes, año));
        return Ok(lista);

    }

}