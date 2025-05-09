using ClinicaAPI.Models.Usuario.Medico;
using ClinicaAPI.Repository.DAO;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicoController : ControllerBase
    {
        [HttpGet("listaMedicos")]
        public async Task<ActionResult<List<Medico>>> listaMedicos()
        {
            var lista = await Task.Run(() 
                => new MedicoDAO().listarMedicosFront());
            return Ok(lista);
        }

        [HttpGet("listaMedicosBackend")]
        public async Task<ActionResult<List<MedicoO>>> listaMedicosBackend()
        {
            var lista = await Task.Run(()
                => new MedicoDAO().listarMedicosBack());
            return Ok(lista);
        }

        [HttpPost("nuevoMedico")]
        public async Task<ActionResult<string>> guardarMedico(MedicoO medicoO)
        {
            var mensaje = await Task.Run(() 
                => new MedicoDAO().agregarMedico(medicoO));
            return Ok(mensaje);
        }

        [HttpGet("buscarMedico/{id}")]
        public async Task<ActionResult<MedicoO>> buscarMedicoPorId(long id)
        {
            var lista = await Task.Run(() 
                => new MedicoDAO().buscarMedicoPorID(id));
            return Ok(lista);
        }

        [HttpPut("actualizarMedico")]
        public async Task<ActionResult<string>> actualizarMedico(MedicoO medicoO)
        {
            var mensaje = await Task.Run(() 
                => new MedicoDAO().actualizarMedicoPorID(medicoO));
            return Ok(mensaje);
        }

        [HttpDelete("eliminarMedico/{id}")]
        public async Task<ActionResult> eliminarMedico(long id)
        {
            var lista = await Task.Run(()
                => new MedicoDAO().eliminarMedicoPorID(id));
            return Ok(lista);
        }

        [HttpGet("[action]")]
        public async Task<ActionResult<List<Especialidad>>> listarEspecialidad()
        {
            var lista = await Task.Run(()
                => new EspecialidadDAO().listarEspecialidad());
            return Ok(lista);
        }
        [HttpGet("listaCitaPorMedicos/{ide_usr}")]
        public ActionResult<List<CitaMedico>> listaCitaPorMedicos(long ide_usr)
        {
            var lista = new MedicoDAO().listarCitaMedico(ide_usr);
            return Ok(lista);
        }
    }
}
