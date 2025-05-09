using System.ComponentModel;

namespace ClinicaWebApp.Models.Usuario.Paciente;

public class PacienteO : UsuarioO
{
    [DisplayName("ID PACIENTE")]
    public long ide_pac { get; set; }
    [DisplayName("ID USUARIO")]
    public long ide_usr { get; set; }
}