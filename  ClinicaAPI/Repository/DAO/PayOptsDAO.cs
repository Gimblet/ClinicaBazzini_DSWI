using System.Data;
using System.Data.SqlClient;
using ClinicaAPI.Models.Pago;
using ClinicaAPI.Repository.Interfaces;

namespace ClinicaAPI.Repository.DAO;

public class PayOptsDAO : IPayOpts
{
    private readonly string _connectionString;

    public PayOptsDAO()
    {
        _connectionString = new ConfigurationBuilder().AddJsonFile("appsettings.json")
            .Build().GetConnectionString("cn") ?? throw new NullReferenceException();
    }

    public IEnumerable<PayOpts> ListarTiposDePago()
    {
        List<PayOpts> lista = new List<PayOpts>();
        SqlConnection cn = new SqlConnection(_connectionString);
        SqlCommand cmd = new SqlCommand("sp_listarPaymentOptions", cn);
        cmd.CommandType = CommandType.StoredProcedure;
        try
        {
            cn.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                lista.Add(new PayOpts
                {
                    ide_pay = int.Parse(dr[0].ToString()),
                    nom_pay = dr[1].ToString()
                });
            }
        }
        catch (Exception e)
        {
            Console.Write("Ocurrió un error al intentar listar los Tipos de Pago");
            Console.WriteLine(e.Message);
        }
        finally
        {
            cn.Close();
        }
        return lista;
    }
}