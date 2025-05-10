using System.ComponentModel;

namespace ClinicaWebApp.Models.Pago;

public class Pago
{
    [DisplayName("Nro. Pago")]
    public long IdPago { get; set; }

    [DisplayName("Fecha")]
    public DateTime HoraPago { get; set; }

    [DisplayName("Monto")]
    public decimal MontoPago { get; set; }

    [DisplayName("Modalidad")]
    public string? TipoPago { get; set; }

    [DisplayName("Cliente")]
    public string? NombrePaciente { get; set; }

    [DisplayName("E-mail")]
    public string? CorreoPaciente { get; set; }
}