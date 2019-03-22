using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TipoCambioSunat.Models;

namespace TipoCambioSunat.Repositories
{
    public interface ITipoCambioRepository
    {
        Task<TipoCambio> fGetPorDia(int day, int month, int year, CancellationToken cancellationToken);
        Task<List<TipoCambio>> fGetPorMes(int month, int year, CancellationToken cancellationToken);
    }
}
