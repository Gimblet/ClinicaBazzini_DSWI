using System.Data;
using System.Data.SqlClient;
using ClinicaAPI.Models.Usuario.Medico;
using ClinicaAPI.Repository.Interfaces;

namespace ClinicaAPI.Repository.DAO;

public class EspecialidadDAO : IEspecialidad
{
    private readonly string _connectionString;

    public EspecialidadDAO()
    {
        _connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json")
            .Build().GetConnectionString("cn") ?? throw new NullReferenceException();
    }

    public IEnumerable<Especialidad> listarEspecialidad()
    {
        List<Especialidad> listaEspecialidad = new List<Especialidad>();
        SqlConnection cn = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand("sp_listarEspecialidad", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            listaEspecialidad.Add(new Especialidad
            {
                ide_esp = long.Parse(dr[0].ToString()),
                nom_esp = dr[1].ToString(),
            });
        }

        cn.Close();
        return listaEspecialidad;
    }
}