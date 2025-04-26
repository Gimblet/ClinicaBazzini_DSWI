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
        throw new NotImplementedException();
    }
}