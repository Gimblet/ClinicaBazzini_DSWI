using System.Data;
using System.Data.SqlClient;
using ClinicaAPI.Models.Pago;
using ClinicaAPI.Repository.Interfaces;

namespace ClinicaAPI.Repository.DAO;

public class PagoDAO : IPago
{
    private readonly string _connectionString;

    public PagoDAO()
    {
        _connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json")
            .Build().GetConnectionString("cn") ?? throw new NullReferenceException();
    }

    public IEnumerable<Pago> ListarPagos()
    {
        List<Pago> pagos = new List<Pago>();
        SqlConnection cn = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand("sp_listarPagos", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                pagos.Add(new Pago()
                {
                    IdPago = int.Parse(dr[0].ToString()),
                    HoraPago = DateTime.Parse(dr[1].ToString()),
                    MontoPago = decimal.Parse(dr[2].ToString()),
                    TipoPago = dr[3].ToString(),
                    CorreoPaciente = dr[4].ToString(),
                    NombrePaciente = dr[5].ToString()
                });
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

        return pagos;
    }

    public IEnumerable<Pago> ListarPagosPorPaciente(long id)
    {
        List<Pago> pagos = new List<Pago>();
        SqlConnection cn = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand("sp_listarPagosPorPaciente", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ide", id);
        try
        {
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                pagos.Add(new Pago()
                {
                    IdPago = int.Parse(dr[0].ToString()),
                    HoraPago = DateTime.Parse(dr[1].ToString()),
                    MontoPago = decimal.Parse(dr[2].ToString()),
                    TipoPago = dr[3].ToString(),
                    CorreoPaciente = dr[4].ToString(),
                    NombrePaciente = dr[5].ToString()
                });
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

        return pagos;
    }

    public long AgregarPago(PagoO pago, long token)
    {
        long idGenerado = 0;
        SqlConnection cn = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand("sp_agregarPago", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@hora", pago.HoraPago);
        cmd.Parameters.AddWithValue("@monto", pago.MontoPago);
        cmd.Parameters.AddWithValue("@tipopago", pago.TipoPago);
        cmd.Parameters.AddWithValue("@usuario", token);
        try
        {
            cn.Open();
            var result = cmd.ExecuteScalar();
            if (result != null) idGenerado = Convert.ToInt64(result);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        finally
        {
            cn.Close();
        }

        return idGenerado;
    }

    public PagoO ObtenerPagoPorId(long id)
    {
        PagoO pago = null;
        SqlConnection cn = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand("sp_obtenerPagoPorId ", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@id", id);
        try
        {
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                pago = new PagoO()
                {
                    IdPago = int.Parse(dr[0].ToString()),
                    HoraPago = DateTime.Parse(dr[1].ToString()),
                    MontoPago = decimal.Parse(dr[2].ToString()),
                    TipoPago = int.Parse(dr[3].ToString()),
                    IdPaciente = int.Parse(dr[4].ToString())
                };
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

        return pago;
    }

    public Pago ObtenerPagoPorIdFront(long id)
    {
        Pago pago = null;
        SqlConnection cn = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand("sp_obtenerPagoPorIdFront", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@id", id);
        try
        {
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                pago = new Pago()
                {
                    IdPago = int.Parse(dr[0].ToString()),
                    HoraPago = DateTime.Parse(dr[1].ToString()),
                    MontoPago = decimal.Parse(dr[2].ToString()),
                    TipoPago = dr[3].ToString(),
                    NombrePaciente = dr[4].ToString(),
                    CorreoPaciente = dr[5].ToString()
                };
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

        return pago;
    }

    public string ActualizarPago(PagoO pago)
    {
        string respuesta = "";
        SqlConnection cn = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand("sp_actualizarPago", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@paciente", pago.IdPaciente);
        cmd.Parameters.AddWithValue("@ide_pag", pago.IdPago);
        cmd.Parameters.AddWithValue("@hor_pag", pago.HoraPago);
        cmd.Parameters.AddWithValue("@mon_pag", pago.MontoPago);
        cmd.Parameters.AddWithValue("@tip_pag", pago.TipoPago);
        try
        {
            cn.Open();
            cmd.ExecuteNonQuery();
            respuesta = "Pago Actualizado correctamente";
        }
        catch (Exception ex)
        {
            respuesta = "Error al Actualizar el pago";
            Console.WriteLine(ex.Message);
        }
        finally
        {
            cn.Close();
        }

        return respuesta;
    }

    public string EliminarPago(long id)
    {
        string respuesta = "";
        SqlConnection cn = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand("sp_eliminarPago", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@id", id);
        try
        {
            cn.Open();
            cmd.ExecuteNonQuery();
            respuesta = "Pago eliminado correctamente";
        }
        catch (Exception ex)
        {
            respuesta = "Error al eliminar el pago";
            Console.WriteLine(ex.Message);
        }
        finally
        {
            cn.Close();
        }
        return respuesta;
    }
}