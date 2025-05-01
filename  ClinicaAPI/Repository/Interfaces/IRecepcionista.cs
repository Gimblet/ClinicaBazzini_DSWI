using ClinicaAPI.Models.Usuario.Recepcionista;

namespace ClinicaAPI.Repository.Interfaces;

public interface IRecepcionista
{
    IEnumerable<Recepcionista> ListarRecepcionistas();
    string agregarRecepcionista(RecepcionistaO recepcionista);
}