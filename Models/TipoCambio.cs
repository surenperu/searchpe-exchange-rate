using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TipoCambioSunat.Models
{
    public class TipoCambio
    {
        public string Dia { get; set; }
        public string Mes { get; set; }
        public string Anho { get; set; }   
        public double Compra { get; set; }
        public double Venta { get; set; }
        public DateTimeOffset Modified { get; set; }
    }
}
