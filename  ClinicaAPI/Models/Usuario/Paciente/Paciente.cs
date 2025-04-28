namespace ClinicaAPI.Models.Usuario.Paciente;

public class Paciente
{
    public long IdPaciente { get; set; }
    public string? NombrePaciente { get; set; }
    public string? ApellidoPaciente { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public string? TipoDocumento { get; set; }
    public string? NumeroDocumento { get; set; }
    public string? Rol { get; set; }
    
}