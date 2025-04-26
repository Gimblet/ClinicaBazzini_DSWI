using ClinicaAPI.Models.Cita;
using ClinicaAPI.Repository.DAO;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CitaController : Controller
{
    [HttpGet("listaCita")]
    public async Task<ActionResult<List<Cita>>> listaCita()
    {
        var lista = await Task.Run(()
            => new CitaDAO().listarCitas());
        return Ok(lista);
    }
}