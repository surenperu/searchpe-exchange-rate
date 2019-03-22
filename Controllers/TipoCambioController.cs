using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading;
using System.Threading.Tasks;
using TipoCambioSunat.Commands;
using TipoCambioSunat.Constants;
using TipoCambioSunat.ViewModels;

namespace TipoCambioSunat.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class TipoCambioController : ControllerBase
    {
        [HttpGet("{year}/{month}/{day}", Name = TipoCambioControllerRoute.GetTipoCambio)]
        [SwaggerResponse(StatusCodes.Status200OK, "The exchange rate with the specified unique identifier.", typeof(TipoCambio))]
        [SwaggerResponse(StatusCodes.Status304NotModified, "The  exchange rate has not changed since the date given in the If-Modified-Since HTTP header.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A  exchange rate with the specified unique identifier could not be found.")]
        public Task<IActionResult> GetByDay([FromServices] IGetTipoCambioCommand command, int year, int month, int day, CancellationToken cancellationToken) => command.ExecuteAsync(year, month, day);


        [HttpGet("{year}/{month}", Name = TipoCambioControllerRoute.GetListTipoCambio)]
        [SwaggerResponse(StatusCodes.Status200OK, "The exchange rate with the specified unique identifier.", typeof(TipoCambio))]
        [SwaggerResponse(StatusCodes.Status304NotModified, "The  exchange rate has not changed since the date given in the If-Modified-Since HTTP header.")]
        [SwaggerResponse(StatusCodes.Status404NotFound, "A  exchange rate with the specified unique identifier could not be found.")]
        public Task<IActionResult> GetByMonth([FromServices] IGetTipoCambioListCommand command, int year, int month, CancellationToken cancellationToken) => command.ExecuteAsync(year, month);
    }
}