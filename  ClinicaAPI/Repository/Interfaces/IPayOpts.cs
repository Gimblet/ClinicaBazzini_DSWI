using ClinicaAPI.Models.Pago;

namespace ClinicaAPI.Repository.Interfaces;

public interface IPayOpts
{
    IEnumerable<PayOpts> ListarTiposDePago();
}