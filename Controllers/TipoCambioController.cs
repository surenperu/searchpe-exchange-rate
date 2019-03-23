using Microsoft.AspNetCore.Mvc;
using searchpe_exchange_rate.Commands;
using searchpe_exchange_rate.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace searchpe_exchange_rate.Controllers
{
    [Route("api/exchange-rate")]
    [ApiController]
    public class TipoCambioController : ControllerBase
    {
        // GET api/values
        [HttpGet("{year}/{month}")]
        public async Task<ActionResult<List<TipoCambio>>> GetAllAsync(int year, int month)
        {
            try
            {
                TipoCambioCommand command = new TipoCambioCommand();
                return await command.fGetPorMes(month, year);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // GET api/values/5
        [HttpGet("{year}/{month}/{day}")]
        public async Task<ActionResult<TipoCambio>> GetAsync(int year, int month, int day)
        {
            try
            {
                TipoCambioCommand command = new TipoCambioCommand();
                return await command.fGetPorDia(day, month, year);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}