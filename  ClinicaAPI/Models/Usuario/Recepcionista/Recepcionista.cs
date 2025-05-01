namespace ClinicaAPI.Models.Usuario.Recepcionista;

public class Recepcionista
{
    public long IdRecepcionista { get; set; }
    public string? NombreRecepcionista { get; set; }
    public string? ApellidoRecepcionista { get; set; }
    public DateTime FechaNacimiento { get; set; }
    public string? TipoDocumento { get; set; }
    public string? NumeroDocumento { get; set; }
    public string? Rol { get; set; }
    public decimal? Sueldo { get; set; }
}