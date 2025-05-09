using ClinicaAPI.Models.Usuario.Medico;

namespace ClinicaAPI.Repository.Interfaces;

public interface IMedico
{
    IEnumerable<Medico> listarMedicosFront();
    IEnumerable<MedicoO> listarMedicosBack();
    string agregarMedico(MedicoO medico);
    Medico buscarMedicoPorID(long id);
    string actualizarMedicoPorID(MedicoO medico);
    string eliminarMedicoPorID(long id);
    IEnumerable<CitaMedico> listarCitaMedico(long ide_usr);
}