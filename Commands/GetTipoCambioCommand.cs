using Boxed.Mapping;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Net.Http.Headers;
using System;
using System.Threading;
using System.Threading.Tasks;
using TipoCambioSunat.Repositories;
using TipoCambioSunat.ViewModels;

namespace TipoCambioSunat.Commands
{
    public class GetTipoCambioCommand : IGetTipoCambioCommand
    {
        private readonly IActionContextAccessor actionContextAccessor;
        private readonly ITipoCambioRepository tipoCambioRepository;
        private readonly IMapper<Models.TipoCambio, TipoCambio> tipoCambioMapper;

        public GetTipoCambioCommand(
            IActionContextAccessor actionContextAccessor,
            ITipoCambioRepository tipoCambioRepository,
            IMapper<Models.TipoCambio, TipoCambio> tipoCambioMapper)
        {
            this.actionContextAccessor = actionContextAccessor;
            this.tipoCambioRepository = tipoCambioRepository;
            this.tipoCambioMapper = tipoCambioMapper;
        }
        public async Task<IActionResult> ExecuteAsync(int year, int month, int day, CancellationToken cancellationToken)
        {
            var tipocambio = await this.tipoCambioRepository.fGetPorDia(day, month, year, cancellationToken);
            if (tipocambio == null)
            {
                return new NotFoundResult();
            }

            var httpContext = this.actionContextAccessor.ActionContext.HttpContext;
            if (httpContext.Request.Headers.TryGetValue(HeaderNames.IfModifiedSince, out var stringValues))
            {
                if (DateTimeOffset.TryParse(stringValues, out var modifiedSince) &&
                    (modifiedSince >= tipocambio.Modified))
                {
                    return new StatusCodeResult(StatusCodes.Status304NotModified);
                }
            }

            var carViewModel = this.tipoCambioMapper.Map(tipocambio);
            httpContext.Response.Headers.Add(HeaderNames.LastModified, tipocambio.Modified.ToString("R"));
            return new OkObjectResult(carViewModel);
        }
    }
}
