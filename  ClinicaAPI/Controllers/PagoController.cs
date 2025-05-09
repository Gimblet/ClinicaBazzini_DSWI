using ClinicaAPI.Models.Pago;
using ClinicaAPI.Repository.DAO;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PagoController : Controller
{
    [HttpGet("[action]")]
    public async Task<ActionResult<List<PayOpts>>> ObtenerTiposDePago()
    {
        var lista = await Task.Run(()
            => new PayOptsDAO().ListarTiposDePago());
        return Ok(lista);
    }

    [HttpGet("[action]")]
    public async Task<ActionResult<List<Pago>>> ListarPagosGeneral()
    {
        var lista = await Task.Run(()
            => new PagoDAO().ListarPagos());
        return Ok(lista);
    }

    [HttpGet("[action]")]
    public async Task<ActionResult<List<Pago>>> ListarPagosPorPaciente(long token)
    {
        var lista = await Task.Run(()
            => new PagoDAO().ListarPagosPorPaciente(token));
        return Ok(lista);
    }


    [HttpPost("[action]/{token}")]
    public async Task<ActionResult<string>> AgregarPago(PagoO pago, long token)
    {
        var idPago = await Task.Run(()
            => new PagoDAO().AgregarPago(pago, token));
        return Ok(idPago);
    }

    [HttpGet("[action]/{id}")]
    public async Task<ActionResult<PagoO>> ObtenerPagoPorId(long id)
    {
        var lista = await Task.Run(()
            => new PagoDAO().ObtenerPagoPorId(id));
        return Ok(lista);
    }

    [HttpPut("[action]")]
    public async Task<ActionResult<string>> ActualizarPago(PagoO pago)
    {
        var lista = await Task.Run(()
            => new PagoDAO().ActualizarPago(pago));
        return Ok(lista);
    }

    [HttpDelete("[action]")]
    public async Task<ActionResult<string>> EliminarPago(long id)
    {
        var lista = await Task.Run(()
            => new PagoDAO().EliminarPago(id));
        return Ok(lista);
    }
}