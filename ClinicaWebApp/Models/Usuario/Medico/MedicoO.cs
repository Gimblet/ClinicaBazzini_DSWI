using System.ComponentModel;

namespace ClinicaWebApp.Models.Usuario.Medico;

public class MedicoO : UsuarioO
{
    [DisplayName("ID")]
    public long ide_med { get; set; }

    [DisplayName("Sueldo")]
    public decimal sue_med { get; set; }

    [DisplayName("Especialidad")]
    public long ide_esp { get; set; }

    [DisplayName("ID Usr.")]
    public long ide_usr { get; set; }
}