using System.Data;
using System.Data.SqlClient;
using ClinicaAPI.Models.Usuario;
using ClinicaAPI.Models.Usuario.Paciente;
using ClinicaAPI.Repository.Interfaces;

namespace ClinicaAPI.Repository.DAO;

public class PacienteDAO : IPaciente
{
    private readonly string _connectionString;

    public PacienteDAO()
    {
        _connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json")
            .Build().GetConnectionString("cn") ?? throw new NullReferenceException();
    }

    public string GuardarPacienteO(UsuarioO usuario)
    {
        string mensaje = "";
        SqlConnection cn = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand("sp_agregarPaciente", cn);
        try
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@cor", usuario.cor_usr);
            cmd.Parameters.AddWithValue("@pwd", usuario.pwd_usr);
            cmd.Parameters.AddWithValue("@nom", usuario.nom_usr);
            cmd.Parameters.AddWithValue("@ape", usuario.ape_usr);
            cmd.Parameters.AddWithValue("@ndo", usuario.num_doc);
            cmd.Parameters.AddWithValue("@fna", usuario.fna_usr);
            cmd.Parameters.AddWithValue("@doc", usuario.ide_doc);

            cn.Open();
            cmd.ExecuteNonQuery();
            mensaje = "Paciente guardado correctamente";
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

    public IEnumerable<Paciente> ListarPacientes()
    {
        List<Paciente> listaPacientes = new List<Paciente>();
        SqlConnection cn = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand("sp_listarPacientesFront", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            listaPacientes.Add(new Paciente()
            {
                IdPaciente = long.Parse(dr[0].ToString()),
                NombrePaciente = dr[1].ToString(),
                ApellidoPaciente = dr[2].ToString(),
                FechaNacimiento = DateTime.Parse(dr[3].ToString()),
                TipoDocumento = dr[4].ToString(),
                NumeroDocumento = dr[5].ToString(),
                Rol = dr[6].ToString(),
            });
        }
        return listaPacientes;    
    }
}