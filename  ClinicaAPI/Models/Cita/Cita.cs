namespace ClinicaAPI.Models.Cita;

public class Cita
{
    public long IdCita { get; set; }
    public DateOnly CalendarioCita { get; set; }
    public long Consultorio { get; set; }
    public TimeOnly HoraCita { get; set; }
    public string nombreMedico { get; set; }
    public string nombrePaciente { get; set; }
    public decimal montoPago { get; set; }
}