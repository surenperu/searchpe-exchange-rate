using Boxed.Mapping;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TipoCambioSunat.Repositories;
using TipoCambioSunat.ViewModels;

namespace TipoCambioSunat.Commands
{
    public class GetTipoCambioListCommand : IGetTipoCambioListCommand
    {
        private readonly ITipoCambioRepository tipoCambioRepository;
        private readonly IMapper<Models.TipoCambio, TipoCambio> tipoCambioMapper;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IUrlHelper urlHelper;

        public GetTipoCambioListCommand(
            ITipoCambioRepository tipoCambioRepository,
            IMapper<Models.TipoCambio, TipoCambio> tipoCambioMapper,
            IHttpContextAccessor httpContextAccessor,
            IUrlHelper urlHelper)
        {
            this.tipoCambioRepository = tipoCambioRepository;
            this.tipoCambioMapper = tipoCambioMapper;
            this.httpContextAccessor = httpContextAccessor;
            this.urlHelper = urlHelper;
        }
        public async Task<IActionResult> ExecuteAsync(int year, int month, CancellationToken cancellationToken = default)
        {
            var tipoCambios = await this.tipoCambioRepository.fGetPorMes(month, year, cancellationToken);
            if (tipoCambios == null)
            {
                return new NotFoundResult();
            }

            List<TipoCambio> tipoCambioViewModels = this.tipoCambioMapper.MapList(tipoCambios);
            return new OkObjectResult(tipoCambioViewModels);
        }
    }
}
