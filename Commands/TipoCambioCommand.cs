using HtmlAgilityPack;
using searchpe_exchange_rate.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace searchpe_exchange_rate.Commands
{
    public class TipoCambioCommand
    {
        private const string URL = "http://www.sunat.gob.pe/cl-at-ittipcam/tcS01Alias?mes={0}&anho={1}";

        private DataTable obtenerDatos(int month, int year)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Dia", typeof(String));
            dt.Columns.Add("Compra", typeof(String));
            dt.Columns.Add("Venta", typeof(String));

            string urlcomplete = string.Format(URL, string.Format("{0:00}", month), string.Format("{0:0000}", year));
            string html = new WebClient().DownloadString(urlcomplete);

            HtmlDocument document = new HtmlDocument();
            document.LoadHtml(html);

            HtmlNodeCollection NodesTr = document.DocumentNode.SelectNodes("//table[@class='class=\"form-table\"']//tr");
            if (NodesTr != null)
            {

                int iNumFila = 0;
                foreach (HtmlNode Node in NodesTr)
                {
                    if (iNumFila > 0)
                    {
                        int iNumColumna = 0;
                        DataRow dr = dt.NewRow();
                        foreach (HtmlNode subNode in Node.Elements("td"))
                        {

                            if (iNumColumna == 0)
                            {
                                dr = dt.NewRow();
                            }

                            string sValue = subNode.InnerHtml.ToString().Trim();
                            sValue = System.Text.RegularExpressions.Regex.Replace(sValue, "<.*?>", " ");
                            dr[iNumColumna] = sValue.Trim();

                            iNumColumna++;

                            if (iNumColumna == 3)
                            {
                                dt.Rows.Add(dr);
                                iNumColumna = 0;
                            }
                        }
                    }
                    iNumFila++;
                }

                dt.AcceptChanges();
            }

            return dt;
        }
        public Task<TipoCambio> fGetPorDia(int day, int month, int year)
        {
            try
            {
                TipoCambio respuesta = new TipoCambio();

                string diaNumero = string.Format("{0:00}", day); ;
                DataTable dt = obtenerDatos(month, year);

                string sCompra = (from DataRow dr in dt.AsEnumerable()
                                  where Convert.ToString(dr["Dia"]) == diaNumero
                                  select Convert.ToString(dr["Compra"])).FirstOrDefault();
                string sVenta = (from DataRow dr in dt.AsEnumerable()
                                 where Convert.ToString(dr["Dia"]) == diaNumero
                                 select Convert.ToString(dr["Venta"])).FirstOrDefault();

                respuesta.Fecha = string.Format("{0:00}", day) + "/" + string.Format("{0:00}", month) + "/" + string.Format("{0:0000}", year);
                if (sCompra.Trim().Length > 0)
                {
                    respuesta.Compra = double.Parse(sCompra);
                }
                if (sVenta.Trim().Length > 0)
                {
                    respuesta.Venta = double.Parse(sVenta);
                }
                respuesta.Dia = day;
                respuesta.Promedio = (respuesta.Compra + respuesta.Venta) / 2D;
                return Task.FromResult(respuesta);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Task<List<TipoCambio>> fGetPorMes(int month, int year)
        {
            try
            {
                List<TipoCambio> lstTc = new List<TipoCambio>();
                DataTable dt = obtenerDatos(month, year);
                foreach (DataRow dr in dt.Rows)
                {
                    string diaNumero = int.Parse(dr["Dia"].ToString()).ToString("00");
                    TipoCambio objTc = new TipoCambio()
                    {
                        Dia = int.Parse(diaNumero),
                        Fecha = diaNumero + "/" + string.Format("{0:00}", month) + "/" + string.Format("{0:0000}", year),
                        Compra = double.Parse(dr["Compra"].ToString()),
                        Venta = double.Parse(dr["Venta"].ToString())
                    };
                    lstTc.Add(objTc);
                }

                return Task.FromResult(lstTc);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
