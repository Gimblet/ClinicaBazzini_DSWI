using ClinicaAPI.Models.Usuario.Medico;

namespace ClinicaAPI.Repository.Interfaces;

public interface IMedico
{
    IEnumerable<Medico> listarMedicos();
    IEnumerable<MedicoO> listarMedicosO();
    string agregarMedico(MedicoO objM);
    string modificarMedico(MedicoO objM);
    MedicoO buscarMedico(long id);
    void eliminarMedico(long id);
}