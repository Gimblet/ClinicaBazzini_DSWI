using System.Data;
using System.Data.SqlClient;
using ClinicaAPI.Models.Usuario.Recepcionista;
using ClinicaAPI.Repository.Interfaces;

namespace ClinicaAPI.Repository.DAO;

public class RecepcionistaDAO : IRecepcionista
{
    private readonly string _connectionString;

    public RecepcionistaDAO()
    {
        _connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json")
            .Build().GetConnectionString("cn") ?? throw new NullReferenceException();
    }

    public IEnumerable<Recepcionista> ListarRecepcionistas()
    {
        List<Recepcionista> listaRecepcionistas = new List<Recepcionista>();
        SqlConnection cn = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand("sp_listarRecepcionistasFront", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            listaRecepcionistas.Add(new Recepcionista()
            {
                IdRecepcionista = long.Parse(dr[0].ToString()),
                NombreUsuario = dr[1].ToString(),
                ApellidoUsuario = dr[2].ToString(),
                FechaNacimiento = DateTime.Parse(dr[3].ToString()),
                TipoDocumento = dr[4].ToString(),
                NumeroDocumento = dr[5].ToString(),
                Rol = dr[6].ToString(),
                Sueldo = decimal.Parse(dr[7].ToString())
            });
        }

        return listaRecepcionistas;
    }

    public string agregarRecepcionista(RecepcionistaO recepcionista)
    {
        string mensaje = "";
        SqlConnection cn = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand("sp_agregarRecepcionista", cn);
        try
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@cor", recepcionista.cor_usr);
            cmd.Parameters.AddWithValue("@pwd", recepcionista.pwd_usr);
            cmd.Parameters.AddWithValue("@nom", recepcionista.nom_usr);
            cmd.Parameters.AddWithValue("@ape", recepcionista.ape_usr);
            cmd.Parameters.AddWithValue("@ndo", recepcionista.num_doc);
            cmd.Parameters.AddWithValue("@fna", recepcionista.fna_usr);
            cmd.Parameters.AddWithValue("@doc", recepcionista.ide_doc);
            cmd.Parameters.AddWithValue("@sue", recepcionista.sue_rep);

            cn.Open();
            cmd.ExecuteNonQuery();
            mensaje = "Recepcionista guardado correctamente";
        }
        catch (SqlException ex)
        {
            Console.WriteLine(ex.Message);
            mensaje = "Error al guardar cliente";
        }
        finally
        {
            cn.Close();
        }

        return mensaje;
    }
}