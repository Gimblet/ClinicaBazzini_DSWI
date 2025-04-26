using System.Data;
using System.Data.SqlClient;
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

    public string GuardarPacienteO(PacienteO paciente)
    {
        string mensaje = "";
        SqlConnection cn = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand("sp_agregarPaciente", cn);
        try
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@nombre", paciente.NombrePaciente);
            cmd.Parameters.AddWithValue("@apellido", paciente.ApellidoPaciente);
            cmd.Parameters.AddWithValue("@numerodoc", paciente.NumeroDocumento);
            cmd.Parameters.AddWithValue("@tipodoc", paciente.TipoDocumento);
            cmd.Parameters.AddWithValue("@fechanac", paciente.fechaNacimiento);
            cmd.Parameters.AddWithValue("@correo", paciente.NombrePaciente);
            cmd.Parameters.AddWithValue("@contraseña", paciente.ContraseñaPaciente);

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
                NumeroDocumento = dr[3].ToString(),
                TipoDocumento = dr[4].ToString(),
                fechaNacimiento = DateTime.Parse(dr[5].ToString()),
                CorreoPaciente = dr[6].ToString(),
                ContraseñaPaciente = dr[7].ToString()
            });
        }
        return listaPacientes;    
    }
}