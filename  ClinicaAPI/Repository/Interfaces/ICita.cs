using ClinicaAPI.Models.Cita;

namespace ClinicaAPI.Repository.Interfaces;

public interface ICita
{
   IEnumerable<Cita> listarCitas();
   IEnumerable<CitaO> listarCitasO();
    string agregarCita(CitaO obj);
    string modificarCita(CitaO obj);
    CitaO buscarCita(long id);
    void eliminarCita(long id);

}