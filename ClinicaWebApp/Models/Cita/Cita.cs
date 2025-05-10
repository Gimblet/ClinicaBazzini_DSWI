using System.ComponentModel;

namespace ClinicaWebApp.Models.Cita;

public class Cita
{
    [DisplayName("ID")]
    public long IdCita { get; set; }

    [DisplayName("Fecha")]
    public DateTime CalendarioCita { get; set; }

    [DisplayName("Nro. Consultorio")]
    public long Consultorio { get; set; }

    [DisplayName("Médico")]
    public string? NombreMedico { get; set; }

    [DisplayName("Paciente")]
    public string? NombrePaciente { get; set; }

    [DisplayName("Precio")]
    public decimal MontoPago { get; set; }
}