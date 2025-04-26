namespace ClinicaWebApp.Models.Usuario.Paciente;

public class Paciente
{
    public long IdPaciente { get; set; }
    public string NombrePaciente { get; set; }
    public string ApellidoPaciente { get; set; }
    public string NumeroDocumento { get; set; }
    public string TipoDocumento { get; set; }
    public DateTime fechaNacimiento { get; set; }
    public string CorreoPaciente { get; set; }
    public string ContraseñaPaciente { get; set; }
    
}