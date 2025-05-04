using System.Data;
using System.Data.SqlClient;
using ClinicaAPI.Models.Usuario;
using ClinicaAPI.Repository.Interfaces;

namespace ClinicaAPI.Repository.DAO;

public class UserDocDAO : IUserDoc
{
    private readonly string _connectionString;

    public UserDocDAO()
    {
        _connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json")
            .Build().GetConnectionString("cn") ?? throw new NullReferenceException();
    }

    public IEnumerable<UserDoc> ListarTiposDeDocumento()
    {
        List<UserDoc> listaDocumentos = new List<UserDoc>();
        SqlConnection cn = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand("sp_listarDocumentos", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            listaDocumentos.Add(new UserDoc
            {
                ide_doc = long.Parse(dr[0].ToString()),
                nom_doc = dr[1].ToString(),
            });
        }

        cn.Close();
        return listaDocumentos;
    }
}