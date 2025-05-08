using ClinicaAPI.Models.Cita;
using ClinicaAPI.Repository.Interfaces;
using System.Data.SqlClient;
using System.Data;

namespace ClinicaAPI.Repository.DAO;

public class CitaDAO : ICita
{
    private readonly string _connectionString;

    public CitaDAO()
    {
        _connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json")
            .Build().GetConnectionString("cn") ?? throw new NullReferenceException();
    }

    public string agregarCita(CitaO obj)
    {
        string mensaje = "";
        SqlConnection cn = new SqlConnection(_connectionString);
        cn.Open();
        try
        {
            SqlCommand cmd = new SqlCommand("sp_agregarCita", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@calendario", obj.CalendarioCita);
            cmd.Parameters.AddWithValue("@consultorio", obj.Consultorio);
            cmd.Parameters.AddWithValue("@medico", obj.IdMedico);
            cmd.Parameters.AddWithValue("@paciente", obj.IdPaciente);
            cmd.Parameters.AddWithValue("@pago", obj.IdPago);
           int n = cmd.ExecuteNonQuery();
            mensaje = n.ToString() + "Cita registrada";
        }
        catch (Exception exp)
        {
            mensaje = "Error en el registro" + exp.Message;
        }
        cn.Close();

        return mensaje;
    }

    public CitaO buscarCita(long id)
    {

        return listarCitasO().FirstOrDefault(c => c.IdCita == id);

    }

    public Cita buscarCitaFrond(long id)
    {
        return listarCitas().FirstOrDefault(c => c.IdCita == id);
    }


    public void eliminarCita(long id)
    {
        SqlConnection cn = new SqlConnection(_connectionString);
        cn.Open();
        try
        {
            SqlCommand cmd = new SqlCommand("sp_eliminarCita", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("idCita", id);
            cmd.ExecuteNonQuery();
        }
        catch (Exception)
        {
            throw new Exception(message: "Error al Eliminar la Cita");
        }
        cn.Close();
    }

    public IEnumerable<Cita> listarCitaPorFecha(int dia,int mes,int año)
    {
        List<Cita> lista = new List<Cita>();
        SqlConnection cn = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand("sp_obtenerCitasPorFecha", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@dia", dia);
        cmd.Parameters.AddWithValue("@mes", mes);
        cmd.Parameters.AddWithValue("@año", año);
        cn.Open();
        SqlDataReader dr = cmd.ExecuteReader();

        while (dr.Read()) 
        {
            lista.Add(new Cita()
            {
                IdCita = long.Parse(dr[0].ToString()),
                CalendarioCita = DateTime.Parse(dr[1].ToString()),
                Consultorio = long.Parse(dr[2].ToString()),
                NombreMedico = dr[3].ToString(),
                NombrePaciente = dr[4].ToString(),
                MontoPago = decimal.Parse(dr[5].ToString()),
            });
        
        }
        cn.Close();
        return lista;
    }

    public IEnumerable<Cita> listarCitas()
    {
        List<Cita> listaCitas = new List<Cita>();
        SqlConnection cn = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand("sp_listarCitasFront", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            listaCitas.Add(new Cita()
            {
                IdCita = long.Parse(dr[0].ToString()),
                CalendarioCita = DateTime.Parse(dr[1].ToString()),
                Consultorio = long.Parse(dr[2].ToString()),
                NombreMedico = dr[3].ToString(),
                NombrePaciente = dr[4].ToString(),
                MontoPago = decimal.Parse(dr[5].ToString()),
            });
        }
        cn.Close();
        return listaCitas;
    }

    public IEnumerable<CitaO> listarCitasO()
    {
        List<CitaO> listaCitasO = new List<CitaO>();
        SqlConnection cn = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand("sp_listarCitasBack", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cn.Open();
        SqlDataReader dr = cmd.ExecuteReader();
        while (dr.Read())
        {
            listaCitasO.Add(new CitaO()
            {
                IdCita = long.Parse(dr[0].ToString()),
                CalendarioCita = DateTime.Parse(dr[1].ToString()),
                Consultorio = long.Parse(dr[2].ToString()),                
                IdMedico= long.Parse(dr[3].ToString()),
                IdPaciente = long.Parse(dr[4].ToString()),
                IdPago = long.Parse(dr[5].ToString()),
            });
        }
        cn.Close();
        return listaCitasO;
    }

    public string modificarCita(CitaO obj)
    {
        string mensaje = "";
        SqlConnection cn = new SqlConnection(_connectionString);
        cn.Open();
        try
        {
            SqlCommand cmd = new SqlCommand("sp_actualizarCita", cn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@idCita", obj.IdCita);
            cmd.Parameters.AddWithValue("@calendario", obj.CalendarioCita);
            cmd.Parameters.AddWithValue("@consultorio", obj.Consultorio);
            cmd.Parameters.AddWithValue("@medico", obj.IdMedico);
            cmd.Parameters.AddWithValue("@paciente", obj.IdPaciente);
            cmd.Parameters.AddWithValue("pago", obj.IdPago);
            int n = cmd.ExecuteNonQuery();
            mensaje = n.ToString() + "Cita Actualizada";
        }
        catch (Exception exp)
        {
            mensaje = "Error a la hora de Actualizar" + exp.Message;
        }
        cn.Close();

        return mensaje;
    }
}