using ClinicaAPI.Models.Usuario;
using ClinicaAPI.Models.Usuario.Paciente;

namespace ClinicaAPI.Repository.Interfaces;

public interface IPaciente
{
    string GuardarPacienteO(UsuarioO usuario);
    IEnumerable<Paciente> ListarPacientes();
}