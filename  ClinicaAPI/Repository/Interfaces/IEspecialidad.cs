using ClinicaAPI.Models.Usuario.Medico;

namespace ClinicaAPI.Repository.Interfaces;

public interface IEspecialidad
{
    IEnumerable<Especialidad> listarEspecialidad();
}