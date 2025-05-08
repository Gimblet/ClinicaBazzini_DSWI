using System.ComponentModel;

namespace ClinicaWebApp.Models.Usuario.Medico;

public class Especialidad
{
    public long ide_esp { get; set; }

    [DisplayName("Especialidad")]
    public string nom_esp { get; set; }
}