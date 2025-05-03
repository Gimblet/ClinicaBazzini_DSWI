using System.ComponentModel;

namespace ClinicaWebApp.Models.Usuario;

public class UsuarioO
{
    [DisplayName("Correo")]
    public string? cor_usr { get; set; }
    [DisplayName("Contraseña")]
    public string? pwd_usr { get; set; }
    [DisplayName("Nombre")]
    public string? nom_usr { get; set; }
    [DisplayName("Apellido")]
    public string? ape_usr { get; set; }
    [DisplayName("Fecha Nac.")]
    public DateTime fna_usr { get; set; }
    [DisplayName("Número de Doc.")]
    public string? num_doc { get; set; }
    public long ide_doc { get; set; }
    public long ide_rol { get; set; }
}