using System.Data;
using System.Data.SqlClient;
using ClinicaAPI.Models.Usuario.Medico;
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
            cmd.Parameters.AddWithValue("@cor", paciente.cor_usr);
            cmd.Parameters.AddWithValue("@pwd", paciente.pwd_usr);
            cmd.Parameters.AddWithValue("@nom", paciente.nom_usr);
            cmd.Parameters.AddWithValue("@ape", paciente.ape_usr);
            cmd.Parameters.AddWithValue("@ndo", paciente.num_doc);
            cmd.Parameters.AddWithValue("@fna", paciente.fna_usr);
            cmd.Parameters.AddWithValue("@doc", paciente.ide_doc);

            cn.Open();
            cmd.ExecuteNonQuery();
            mensaje = "Paciente guardado correctamente";
        }
        catch (SqlException ex)
        {
            Console.WriteLine(ex.Message);
            mensaje = "Error al guardar paciente";
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
                NombreUsuario = dr[1].ToString(),
                ApellidoUsuario = dr[2].ToString(),
                FechaNacimiento = DateTime.Parse(dr[3].ToString()),
                TipoDocumento = dr[4].ToString(),
                NumeroDocumento = dr[5].ToString(),
                Rol = dr[6].ToString(),
            });
        }

        cn.Close();
        return listaPacientes;
    }

    public IEnumerable<PacienteO> ListarPacientesO()
    {
        List<PacienteO> listaPacientes = new List<PacienteO>();
        SqlConnection cn = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand("sp_listarPacientesBack", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            listaPacientes.Add(new PacienteO()
            {
                ide_usr = int.Parse(dr[0].ToString()),
                ide_pac = int.Parse(dr[1].ToString()),
                cor_usr = dr[2].ToString(),
                pwd_usr = dr[3].ToString(),
                nom_usr = dr[4].ToString(),
                ape_usr = dr[5].ToString(),
                fna_usr = DateTime.Parse(dr[6].ToString()),
                num_doc = dr[7].ToString(),
                ide_doc = int.Parse(dr[8].ToString()),
                ide_rol = int.Parse(dr[9].ToString())
            });
        }
        return listaPacientes;
    }

    public Paciente buscarPacientePorID(long id)
    {
        Paciente paciente = new Paciente();
        SqlConnection cn = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand("sp_buscarPaciente", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@id", id);
        cn.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            paciente = new Paciente()
            {
                IdPaciente = long.Parse(dr[0].ToString()),
                NombreUsuario = dr[1].ToString(),
                ApellidoUsuario = dr[2].ToString(),
                FechaNacimiento = DateTime.Parse(dr[3].ToString()),
                TipoDocumento = dr[4].ToString(),
                NumeroDocumento = dr[5].ToString(),
                Rol = dr[6].ToString(),
            };
        }

        cn.Close();
        return paciente;
    }

    public string actualizarPaciente(PacienteO p)
    {
        string mensaje = "";
        SqlConnection cn = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand("sp_actualizarPaciente", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@id", p.ide_pac);
        cmd.Parameters.AddWithValue("@cor", p.cor_usr);
        cmd.Parameters.AddWithValue("@pwd", p.pwd_usr);
        cmd.Parameters.AddWithValue("@nom", p.nom_usr);
        cmd.Parameters.AddWithValue("@ape", p.ape_usr);
        cmd.Parameters.AddWithValue("@ndo", p.num_doc);
        cmd.Parameters.AddWithValue("@fna", p.fna_usr);
        cmd.Parameters.AddWithValue("@doc", p.ide_doc);
        try
        {
            cn.Open();
            cmd.ExecuteNonQuery();
            mensaje = "Paciente actualizado correctamente";
        }
        catch (Exception ex)
        {
            mensaje = "Error al actualizar paciente " + ex.Message;
        }
        finally
        {
            cn.Close();
        }
        return mensaje;
    }
    public string eliminarPaciente(long id)
    {
        string mensaje = "";
        SqlConnection cn = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand("sp_eliminarPaciente", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@id", id);
        try
        {
            cn.Open();
            cmd.ExecuteNonQuery();
            mensaje = "Paciente eliminado correctamente";
        }
        catch (Exception ex)
        {
            mensaje = "Paciente eliminado correctamente";
            Console.WriteLine(ex.ToString());
        }
        finally
        {
            cn.Close();
        }

        return mensaje;
    }

    public IEnumerable<CitaPaciente> listarCitaPaciente(long ide_usr)
    {
        List<CitaPaciente> listaCitaPaciente = new List<CitaPaciente>();
        SqlConnection cn = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand("sp_listarCitasPorPaciente", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ide_usr", ide_usr);
        cn.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            listaCitaPaciente.Add(new CitaPaciente()
            {
                ide_cit = long.Parse(dr[0].ToString()),
                cal_cit = DateTime.Parse(dr[1].ToString()),
                con_cit = int.Parse(dr[2].ToString()),
                medico = dr[3].ToString(),
                nom_esp= dr[4].ToString(),
                mon_pag = decimal.Parse(dr[5].ToString()),
            });
        }

        cn.Close();
        return listaCitaPaciente;
    }
}