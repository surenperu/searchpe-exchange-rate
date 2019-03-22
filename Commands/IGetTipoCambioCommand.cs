using Boxed.AspNetCore;

namespace TipoCambioSunat.Commands
{
    public interface IGetTipoCambioCommand : IAsyncCommand<int, int, int>
    {
    }
}
