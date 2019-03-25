namespace searchpe_exchange_rate.Models
{
    public class TipoCambio
    {
        public string Fecha { get; set; }
        public int Dia { get; set; }      
        public double Compra { get; set; }
        public double Venta { get; set; }
        public double Promedio { get; set; }
    }
}
