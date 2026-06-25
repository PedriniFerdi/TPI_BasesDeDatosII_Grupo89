using System;
using System.Collections.Generic;
using Padelito.Datos.Repositorios;
using Padelito.Dominio.Entidades;

namespace Padelito.Negocio.Servicios
{
    public class ReporteNegocio
    {
        private readonly ReporteDatos _reporteDatos = new ReporteDatos();

        public List<ReporteReserva> ListarReservasPorFecha(DateTime fechaDesde, DateTime fechaHasta, int? idEstadoReserva)
        {
            ValidarFechas(fechaDesde, fechaHasta);
            return _reporteDatos.ListarReservasPorFecha(fechaDesde, fechaHasta, idEstadoReserva);
        }

        private static void ValidarFechas(DateTime fechaDesde, DateTime fechaHasta)
        {
            if (fechaDesde == DateTime.MinValue)
            {
                throw new ArgumentException("Debe ingresar la fecha desde.");
            }

            if (fechaHasta == DateTime.MinValue)
            {
                throw new ArgumentException("Debe ingresar la fecha hasta.");
            }

            if (fechaHasta.Date < fechaDesde.Date)
            {
                throw new ArgumentException("La fecha hasta debe ser mayor o igual a la fecha desde.");
            }
        }
    }
}
