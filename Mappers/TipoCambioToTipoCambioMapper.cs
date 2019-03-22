using Boxed.Mapping;
using Microsoft.AspNetCore.Mvc;
using System;

namespace TipoCambioSunat.Mappers
{
    public class TipoCambioToTipoCambioMapper : IMapper<Models.TipoCambio, ViewModels.TipoCambio>
    {
        private readonly IUrlHelper urlHelper;

        public TipoCambioToTipoCambioMapper(IUrlHelper urlHelper) => this.urlHelper = urlHelper;

        public void Map(Models.TipoCambio source, ViewModels.TipoCambio destination)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (destination == null)
            {
                throw new ArgumentNullException(nameof(destination));
            }

            destination.Compra = source.Compra;
            destination.Fecha = source.Dia + "/" + source.Mes + "/" + source.Anho;
            destination.Venta = source.Venta;
        }
    }
}
