using ClinicaAPI.Models.Usuario.Medico;
using ClinicaAPI.Repository.Interfaces;

namespace ClinicaAPI.Repository.DAO;

public class MedicoDAO : IMedico
{
    private readonly string _connectionString;

    public MedicoDAO()
    {
        _connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json")
            .Build().GetConnectionString("cn") ?? throw new NullReferenceException();
    }
    public string agregarMedico(MedicoO objM)
    {
        throw new NotImplementedException();
    }

    public MedicoO buscarMedico(long id)
    {
        throw new NotImplementedException();
    }

    public void eliminarMedico(long id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Medico> listarMedicos()
    {
        throw new NotImplementedException();
    }

    public IEnumerable<MedicoO> listarMedicosO()
    {
        throw new NotImplementedException();
    }

    public string modificarMedico(MedicoO objM)
    {
        throw new NotImplementedException();
    }
}