namespace ClinicaAPI.Models.Usuario.Medico;

public class Medico : UsuarioO
{
    public long IdMedico { get; set; }
    public decimal sueldo { get; set; }
    public string especialidad { get; set; }
}