using ClinicaAPI.Models.Usuario.Paciente;

namespace ClinicaAPI.Repository.Interfaces;

public interface IPaciente
{
    string GuardarPacienteO(PacienteO paciente);
    IEnumerable<Paciente> ListarPacientes();
}