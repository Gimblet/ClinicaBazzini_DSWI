using ClinicaAPI.Models.Usuario.Recepcionista;
using ClinicaAPI.Repository.DAO;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class RecepcionistaController : Controller
{
    [HttpPost("[action]")]
    public async Task<ActionResult<string>> GuardarRecepcionista(RecepcionistaO recepcionistaO)
    {
        var mensaje = await Task.Run(()
            => new RecepcionistaDAO().agregarRecepcionista(recepcionistaO));
        return Ok(mensaje);
    }

    [HttpGet("[action]")]
    public async Task<ActionResult<List<Recepcionista>>> ListarRecepcionistasFront()
    {
        var lista = await Task.Run(()
            => new RecepcionistaDAO().ListarRecepcionistasFront());
        return Ok(lista);
    }
    
    [HttpGet("[action]")]
    public async Task<ActionResult<List<RecepcionistaO>>> ListarRecepcionistasBack()
    {
        var lista = await Task.Run(()
            => new RecepcionistaDAO().ListarRecepcionistasBack());
        return Ok(lista);
    }
    
    [HttpGet("[action]/{id}")]
    public async Task<ActionResult<RecepcionistaO>> BuscarRecepcionistaPorId(long id)
    {
        var lista = await Task.Run(()
            => new RecepcionistaDAO().BuscarRecepcionistaPorID(id));
        return Ok(lista);
    }
    
    [HttpPut("[action]")]
    public async Task<ActionResult<string>> ActualizarRecepcionista(RecepcionistaO recepcionistaO)
    {
        var lista = await Task.Run(()
            => new RecepcionistaDAO().actualizarRecepcionistaPorID(recepcionistaO));
        return Ok(lista);
    }
    
    [HttpDelete("[action]/{id}")]
    public async Task<ActionResult<string>> EliminarRecepcionistaPorId(long id)
    {
        var lista = await Task.Run(()
            => new RecepcionistaDAO().eliminarRecepcionistaPorID(id));
        return Ok(lista);
    }
}