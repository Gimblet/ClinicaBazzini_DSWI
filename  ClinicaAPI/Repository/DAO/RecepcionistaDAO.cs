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

    public IEnumerable<Recepcionista> ListarRecepcionistasFront()
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

        cn.Close();
        return listaRecepcionistas;
    }

    public IEnumerable<RecepcionistaO> ListarRecepcionistasBack()
    {
        List<RecepcionistaO> listaRecepcionistas = new List<RecepcionistaO>();
        SqlConnection cn = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand("sp_listarRecepcionistasBack", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            listaRecepcionistas.Add(new RecepcionistaO()
            {
                ide_usr = long.Parse(dr[0].ToString()),
                ide_rep = long.Parse(dr[1].ToString()),
                sue_rep = decimal.Parse(dr[2].ToString()),
                cor_usr = dr[3].ToString(),
                pwd_usr = dr[4].ToString(),
                nom_usr = dr[5].ToString(),
                ape_usr = dr[6].ToString(),
                fna_usr = DateTime.Parse(dr[7].ToString()),
                num_doc = dr[8].ToString(),
                ide_doc = long.Parse(dr[9].ToString()),
                ide_rol = long.Parse(dr[10].ToString())
            });
        }

        cn.Close();
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
            mensaje = "Error al guardar recepcionista";
        }
        finally
        {
            cn.Close();
        }

        return mensaje;
    }

    public Recepcionista BuscarRecepcionistaPorID(long id)
    {
        Recepcionista recepcionista = new Recepcionista();
        SqlConnection cn = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand("sp_buscarRecepcionistaPorId", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@id", id);
        cn.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            recepcionista = new Recepcionista()
            {
                IdRecepcionista = long.Parse(dr[0].ToString()),
                NombreUsuario = dr[1].ToString(),
                ApellidoUsuario = dr[2].ToString(),
                FechaNacimiento = DateTime.Parse(dr[3].ToString()),
                TipoDocumento = dr[4].ToString(),
                NumeroDocumento = dr[5].ToString(),
                Rol = dr[6].ToString(),
                Sueldo = decimal.Parse(dr[7].ToString())
            };
        }

        cn.Close();
        return recepcionista;
    }

    public string actualizarRecepcionistaPorID(RecepcionistaO recepcionista)
    {
        string mensaje = "";
        SqlConnection cn = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand("sp_actualizarRecepcionista", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@id", recepcionista.ide_rep);
        cmd.Parameters.AddWithValue("@sue", recepcionista.sue_rep);
        cmd.Parameters.AddWithValue("@cor", recepcionista.cor_usr);
        cmd.Parameters.AddWithValue("@pwd", recepcionista.pwd_usr);
        cmd.Parameters.AddWithValue("@nom", recepcionista.nom_usr);
        cmd.Parameters.AddWithValue("@ape", recepcionista.ape_usr);
        cmd.Parameters.AddWithValue("@ndo", recepcionista.num_doc);
        cmd.Parameters.AddWithValue("@fna", recepcionista.fna_usr);
        cmd.Parameters.AddWithValue("@doc", recepcionista.ide_doc);
        try
        {
            cn.Open();
            cmd.ExecuteNonQuery();
            mensaje = "Recepcionista actualizada correctamente";
        }
        catch (Exception ex)
        {
            mensaje = "Error al actualizar recepcionista";
            Console.WriteLine(ex.ToString());
        }
        finally
        {
            cn.Close();
        }

        return mensaje;
    }

    public string eliminarRecepcionistaPorID(long id)
    {
        string mensaje = "";
        SqlConnection cn = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand("sp_eliminarRecepcionista", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@id", id);
        try
        {
            cn.Open();
            cmd.ExecuteNonQuery();
            mensaje = "Recepcionista eliminada correctamente";
        }
        catch (Exception ex)
        {
            mensaje = "Error al eliminar recepcionista";
            Console.WriteLine(ex.ToString());
        }
        finally
        {
            cn.Close();
        }

        return mensaje;
    }
}