using Boxed.AspNetCore;
using System.Collections.Generic;
using TipoCambioSunat.ViewModels;

namespace TipoCambioSunat.Commands
{
    public interface IGetTipoCambioListCommand : IAsyncCommand<int,int>
    {
    }
}
