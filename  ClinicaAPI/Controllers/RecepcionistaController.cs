using ClinicaAPI.Models.Usuario.Recepcionista;
using ClinicaAPI.Repository.DAO;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class RecepcionistaController : Controller
{
    [HttpPost("[action]")]
    public async Task<ActionResult<string>> GuardarRecepcionista(RecepcionistaO r)
    {
        var mensaje = await Task.Run(()
            => new RecepcionistaDAO().agregarRecepcionista(r));
        return Ok(mensaje);
    }

    [HttpGet("[action]")]
    public async Task<ActionResult<List<Recepcionista>>> ListarRecepcionistas()
    {
        var lista = await Task.Run(()
            => new RecepcionistaDAO().ListarRecepcionistas());
        return Ok(lista);
    }
}