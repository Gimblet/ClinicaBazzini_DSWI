using ClinicaAPI.Models.Usuario.Recepcionista;

namespace ClinicaAPI.Repository.Interfaces;

public interface IRecepcionista
{
    IEnumerable<Recepcionista> ListarRecepcionistasFront();
    IEnumerable<RecepcionistaO> ListarRecepcionistasBack();
    string agregarRecepcionista(RecepcionistaO recepcionista);
    Recepcionista BuscarRecepcionistaPorID(long id);
    string actualizarRecepcionistaPorID(RecepcionistaO recepcionista);
    string eliminarRecepcionistaPorID(long id);
}