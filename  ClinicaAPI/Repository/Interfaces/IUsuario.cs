using ClinicaAPI.Models.Usuario;

namespace ClinicaAPI.Repository.Interfaces;

public interface IUsuario
{
    string verificarLogin(string uid, string pwd);
    string obtenerIdUsuario(string correo);
}