using ClinicaAPI.Models.Usuario.Paciente;

namespace ClinicaAPI.Repository.Interfaces;

public interface IPaciente
{   
    IEnumerable<Paciente> ListarPacientes();
    IEnumerable<PacienteO> ListarPacientesO();
    string GuardarPacienteO(PacienteO paciente);
    Paciente buscarPacientePorID(long id);
    string actualizarPaciente(PacienteO paciente);
    string eliminarPaciente(long id);
    

}