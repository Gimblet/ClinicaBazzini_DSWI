namespace ClinicaAPI.Models.Cita;

public class CitaO
{
    public long IdCita { get; set; }
    public DateOnly CalendarioCita { get; set; }
    public long Consultorio { get; set; }
    public TimeOnly HoraCita { get; set; }
    public long IdMedico { get; set; }
    public long IdPaciente { get; set; }
    public long IdPago { get; set; }
}