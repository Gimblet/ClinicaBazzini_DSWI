namespace ClinicaAPI.Models.Pago;

public class Pago
{
    public long IdPago { get; set; }
    public TimeOnly HoraPago { get; set; }
    public decimal MontoPago { get; set; }
    public PayOpts TipoPago { get; set; }
    public string nombrePaciente { get; set; }
}