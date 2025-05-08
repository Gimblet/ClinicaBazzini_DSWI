using System.ComponentModel;

namespace ClinicaWebApp.Models.Usuario.Paciente;

public class Paciente : Usuario
{
    [DisplayName("ID")]
    public long IdPaciente { get; set; }
}