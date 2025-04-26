using ClinicaAPI.Models.Cita;

namespace ClinicaAPI.Repository.Interfaces;

public interface ICita
{
   IEnumerable<Cita> listarCitas();
   IEnumerable<CitaO> listarCitasO();
}