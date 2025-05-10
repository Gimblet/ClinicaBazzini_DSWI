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
                IdMedico = long.Parse(dr["ide_med"].ToString()),
                NombreUsuario = dr["nom_usr"].ToString(),
                ApellidoUsuario = dr["ape_usr"].ToString(),
                FechaNacimiento = DateTime.Parse(dr["fna_usr"].ToString()),
                TipoDocumento = dr["nom_doc"].ToString(),
                NumeroDocumento = dr["num_doc"].ToString(),
                Rol = dr["nom_rol"].ToString(),
                sueldo = decimal.Parse(dr["sue_med"].ToString()),
                especialidad = dr["nom_esp"].ToString()
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
                ide_usr = int.Parse(dr["ide_usr"].ToString()),
                ide_med = int.Parse(dr["ide_med"].ToString()),
                sue_med = decimal.Parse(dr["sue_med"].ToString()),
                ide_esp = int.Parse(dr["ide_esp"].ToString()),
                cor_usr = dr["cor_usr"].ToString(),
                pwd_usr = dr["pwd_usr"].ToString(),
                nom_usr = dr["nom_usr"].ToString(),
                ape_usr = dr["ape_usr"].ToString(),
                fna_usr = DateTime.Parse(dr["fna_usr"].ToString()),
                num_doc = dr["num_doc"].ToString(),
                ide_doc = int.Parse(dr["ide_doc"].ToString()),
                ide_rol = int.Parse(dr["ide_rol"].ToString())
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
                IdMedico = long.Parse(dr["ide_med"].ToString()),
                NombreUsuario = dr["nom_usr"].ToString(),
                ApellidoUsuario = dr["ape_usr"].ToString(),
                FechaNacimiento = DateTime.Parse(dr["fna_usr"].ToString()),
                TipoDocumento = dr["nom_doc"].ToString(),
                NumeroDocumento = dr["num_doc"].ToString(),
                Rol = dr["nom_rol"].ToString(),
                sueldo = decimal.Parse(dr["sue_med"].ToString()),
                especialidad = dr["nom_esp"].ToString()
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

    public IEnumerable<CitaMedico> listarCitaMedico(long ide_usr)
    {
        List<CitaMedico> listaCitaMedicos = new List<CitaMedico>();
        SqlConnection cn = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand("sp_listarCitasPorMedico", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ide_usr", ide_usr);
        cn.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            listaCitaMedicos.Add(new CitaMedico()
            {
                ide_cit = long.Parse(dr["ide_cit"].ToString()),
                cal_cit = DateTime.Parse(dr["cal_cit"].ToString()),
                con_cit = int.Parse(dr["con_cit"].ToString()),
                //medico = dr["medico"].ToString(),
                paciente = dr["paciente"].ToString(),
                mon_pag = decimal.Parse(dr["mon_pag"].ToString()),
            });
        }

        cn.Close();
        return listaCitaMedicos;
    }

    public MedicoStats ObtenerEstadisticasMedico(long ide_usr)
    {
        MedicoStats stats = new MedicoStats();
        SqlConnection cn = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand("sp_totalesPorMedico", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ide_usr", ide_usr);

        cn.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            stats.TotalCitas = Convert.ToInt32(dr["total_citas"]);
            stats.TotalPacientes = Convert.ToInt32(dr["total_pacientes"]);
        }
        cn.Close();

        return stats;
    }

    public IEnumerable<PacientePorMedico> listarPacienteMedico(long ide_usr)
    {
        List<PacientePorMedico> listaPacienteMedicos = new List<PacientePorMedico>();
        SqlConnection cn = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand("sp_listarPacientesPorMedico", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ide_usr", ide_usr);
        cn.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            listaPacienteMedicos.Add(new PacientePorMedico()
            {
                ide_pac = long.Parse(dr["ide_pac"].ToString()),
                Paciente = dr["Paciente"].ToString(),
                num_doc = dr["num_doc"].ToString(),
                fna_usr = DateTime.Parse(dr["fna_usr"].ToString()),
                nom_doc = dr["nom_doc"].ToString(),
                Total_Citas = int.Parse(dr["Total_Citas"].ToString()),
            });
        }

        cn.Close();
        return listaPacienteMedicos;
    }
}