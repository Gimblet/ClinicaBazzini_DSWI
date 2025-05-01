using ClinicaAPI.Models.Usuario.Medico;
using ClinicaAPI.Models.Usuario.Recepcionista;
using ClinicaAPI.Repository.Interfaces;
using System.Data;
using System.Data.SqlClient;

namespace ClinicaAPI.Repository.DAO;

public class MedicoDAO : IMedico
{
    private readonly string _connectionString;

    public MedicoDAO()
    {
        _connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json")
            .Build().GetConnectionString("cn") ?? throw new NullReferenceException();
    }


    public IEnumerable<Medico> listarMedicosFront()
    {
        List<Medico> listaMedicos = new List<Medico>();
        SqlConnection cn = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand("sp_listarMedicosFront", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            listaMedicos.Add(new Medico()
            {
                IdMedico = long.Parse(dr[0].ToString()),
                NombreUsuario = dr[1].ToString(),
                ApellidoUsuario = dr[2].ToString(),
                FechaNacimiento = DateTime.Parse(dr[3].ToString()),
                TipoDocumento = dr[4].ToString(),
                Rol = dr[5].ToString(),
                sueldo = decimal.Parse(dr[6].ToString()),
                especialidad = dr[7].ToString()
            });
        }

        cn.Close();
        return listaMedicos;
    }

    public IEnumerable<MedicoO> listarMedicosBack()
    {
        List<MedicoO> listaMedicos = new List<MedicoO>();
        SqlConnection cn = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand("sp_listarMedicosBack", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            listaMedicos.Add(new MedicoO()
            {
                ide_usr = long.Parse(dr[0].ToString()),
                ide_med = long.Parse(dr[1].ToString()),
                sue_med = decimal.Parse(dr[2].ToString()),
                ide_esp = long.Parse(dr[3].ToString()),
                cor_usr = dr[4].ToString(),
                pwd_usr = dr[5].ToString(),
                nom_usr = dr[6].ToString(),
                ape_usr = dr[7].ToString(),
                fna_usr = DateTime.Parse(dr[8].ToString()),
                num_doc = dr[9].ToString(),
                ide_doc = long.Parse(dr[10].ToString()),
                ide_rol = long.Parse(dr[11].ToString())
            });
        }

        cn.Close();
        return listaMedicos;
    }
    public string agregarMedico(MedicoO medico)
    {
        string mensaje = "";
        SqlConnection cn = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand("sp_agregarMedico", cn);
        try
        {
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@cor", medico.cor_usr);
            cmd.Parameters.AddWithValue("@pwd", medico.pwd_usr);
            cmd.Parameters.AddWithValue("@nom", medico.nom_usr);
            cmd.Parameters.AddWithValue("@ape", medico.ape_usr);
            cmd.Parameters.AddWithValue("@ndo", medico.num_doc);
            cmd.Parameters.AddWithValue("@fna", medico.fna_usr);
            cmd.Parameters.AddWithValue("@doc", medico.ide_doc);
            cmd.Parameters.AddWithValue("@sue", medico.sue_med);
            cmd.Parameters.AddWithValue("@esp", medico.ide_esp);
            cn.Open();
            cmd.ExecuteNonQuery();
            mensaje = "Medico guardado correctamente";
        }
        catch (Exception ex)
        {
            mensaje = "Error al guardar medico" + ex.Message;
        }
        finally
        {
            cn.Close();
        }
        return mensaje;
    }
    public Medico buscarMedicoPorID(long id)
    {
        Medico medico = new Medico();
        SqlConnection cn = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand("sp_buscarMedicoPorId", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@id", id);
        cn.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        if(dr.Read())
        {
            medico = new Medico()
            {
                IdMedico = long.Parse(dr[0].ToString()),
                NombreUsuario = dr[1].ToString(),
                ApellidoUsuario = dr[2].ToString(),
                FechaNacimiento = DateTime.Parse(dr[3].ToString()),
                TipoDocumento = dr[4].ToString(),
                NumeroDocumento = dr[5].ToString(),
                Rol = dr[6].ToString(),
                sueldo = decimal.Parse(dr[7].ToString()),
                especialidad = dr[8].ToString()
            };
        }

        cn.Close();
        return medico;
    }
    public string actualizarMedicoPorID(MedicoO medico)
    {
        string mensaje = "";
        SqlConnection cn = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand("sp_actualizarMedico", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@id", medico.ide_med);
        cmd.Parameters.AddWithValue("@sue", medico.sue_med);
        cmd.Parameters.AddWithValue("@esp", medico.ide_esp);
        cmd.Parameters.AddWithValue("@cor", medico.cor_usr);
        cmd.Parameters.AddWithValue("@pwd", medico.pwd_usr);
        cmd.Parameters.AddWithValue("@nom", medico.nom_usr);
        cmd.Parameters.AddWithValue("@ape", medico.ape_usr);
        cmd.Parameters.AddWithValue("@ndo", medico.num_doc);
        cmd.Parameters.AddWithValue("@fna", medico.fna_usr);
        cmd.Parameters.AddWithValue("@doc", medico.ide_doc);
        try
        {
            cn.Open();
            cmd.ExecuteNonQuery();
            mensaje = "Medico actualizado correctamente";
        }
        catch (Exception ex)
        {
            mensaje = "Error al actualizar medico " + ex.Message;
        }
        finally
        {
            cn.Close();
        }
        return mensaje;
    }

    public string eliminarMedicoPorID(long id)
    {
        string mensaje = "";
        SqlConnection cn = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand("sp_eliminarMedico", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@id", id);
        try
        {
            cn.Open();
            cmd.ExecuteNonQuery();
            mensaje = "Medico eliminado correctamente";
        }
        catch (Exception ex)
        {
            mensaje = "Medico eliminado correctamente";
            Console.WriteLine(ex.ToString());
        }
        finally
        {
            cn.Close();
        }

        return mensaje;
    }

}