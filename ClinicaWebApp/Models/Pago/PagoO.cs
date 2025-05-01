namespace ClinicaWebApp.Models.Pago;

public class PagoO
{
    public long IdPago { get; set; }
    public DateTime HoraPago { get; set; }
    public decimal MontoPago { get; set; }
    public long IdTipoPago { get; set; }
    public long IdPaciente { get; set; }
}