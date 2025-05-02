namespace ClinicaAPI.Models.Pago;

public class Pago
{
    public long IdPago { get; set; }
    public DateTime HoraPago { get; set; }
    public decimal MontoPago { get; set; }
    public string? TipoPago { get; set; }
    public string? NombrePaciente { get; set; }
    public string? CorreoPaciente { get; set; }
}