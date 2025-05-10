using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ClinicaWebApp.Models.Cita;

public class CitaO
{
    public long IdCita { get; set; }

    [DisplayName("Fecha de Cita")]
    [Required(ErrorMessage = "Fecha de Cita es requerida")]
    public DateTime CalendarioCita { get; set; }

    [Required(ErrorMessage = "Consultorio es requerido")]
    public long Consultorio { get; set; }

    [DisplayName("Médico")]
    [Required(ErrorMessage = "Médico es requerido")]
    public long IdMedico { get; set; }

    [DisplayName("Paciente")]
    [Required(ErrorMessage = "Paciente es requerido")]
    public long IdPaciente { get; set; }

    [DisplayName("Pago")]
    [Required(ErrorMessage = "Pago es requerido")]
    public long IdPago { get; set; }
}