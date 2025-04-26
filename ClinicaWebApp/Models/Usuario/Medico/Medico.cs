namespace ClinicaWebApp.Models.Usuario.Medico;

public class Medico
{
    public long IdMedico { get; set; }
    public string NombreMedico { get; set; }
    public string ApellidoMedico { get; set; }
    public string NumeroDocumento { get; set; }
    public string TipoDocumento { get; set; }
    public DateTime fechaNacimiento { get; set; }
    public string CorreoPaciente { get; set; }
    public string ContraseñaPaciente { get; set; }
}