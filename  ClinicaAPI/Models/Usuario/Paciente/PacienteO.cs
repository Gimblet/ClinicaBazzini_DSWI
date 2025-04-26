namespace ClinicaAPI.Models.Usuario.Paciente;

public class PacienteO
{
    public long IdPaciente { get; set; }
    public string NombrePaciente { get; set; }
    public string ApellidoPaciente { get; set; }
    public string NumeroDocumento { get; set; }
    public long TipoDocumento { get; set; }
    public DateTime fechaNacimiento { get; set; }
    public string CorreoPaciente { get; set; }
    public string ContraseñaPaciente { get; set; }
    
}