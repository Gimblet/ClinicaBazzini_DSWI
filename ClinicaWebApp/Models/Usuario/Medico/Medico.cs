using System.ComponentModel;

namespace ClinicaWebApp.Models.Usuario.Medico;

public class Medico : Usuario
{
    [DisplayName("ID")]
    public long IdMedico { get; set; }

    [DisplayName("Sueldo")]
    public decimal sueldo { get; set; }

    [DisplayName("Especialidad")]
    public string? especialidad { get; set; }
}