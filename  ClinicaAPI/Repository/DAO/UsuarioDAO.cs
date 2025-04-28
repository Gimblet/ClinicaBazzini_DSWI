using System.Data;
using System.Data.SqlClient;
using ClinicaAPI.Models.Usuario;
using ClinicaAPI.Repository.Interfaces;

namespace ClinicaAPI.Repository.DAO;

public class UsuarioDAO : IUsuario
{
    private readonly string _connectionString;

    public UsuarioDAO()
    {
        _connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json")
            .Build().GetConnectionString("cn") ?? throw new NullReferenceException();
    }
    
    public string verificarLogin(string uid, string pwd)
    {
        string resultado = "denied";
        SqlConnection cn = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand("sp_verificarLogin", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@correo", uid);
        cmd.Parameters.AddWithValue("@contraseña", pwd);
        try
        {
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (!dr.HasRows)
            {
                throw new Exception("No se ha encontrado el usuario");
            }
            if (dr.Read())
            {
                resultado = dr.GetString(0);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            cn.Close();
        }
        return resultado;
    }
}