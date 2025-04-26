using ClinicaAPI.Models.Pago;
using ClinicaAPI.Repository.DAO;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PagoController : Controller
{
    [HttpGet("ListarTipoDePagos")]
    public async Task<ActionResult<List<PayOpts>>> ObtenerPagos()
    {
        var lista = await Task.Run(()
            => new PayOptsDAO().ListarTiposDePago());
        return Ok(lista);
    }
    
}