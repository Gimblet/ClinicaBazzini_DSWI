using ClinicaAPI.Models.Usuario;

namespace ClinicaAPI.Repository.Interfaces;

public interface IUserDoc
{
    IEnumerable<UserDoc> ListarTiposDeDocumento();
}