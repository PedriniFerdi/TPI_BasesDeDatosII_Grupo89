using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Padelito.Datos.Conexion;
using Padelito.Dominio.Entidades;

namespace Padelito.Datos.Repositorios
{
    public class ReporteDatos
    {
        private readonly ConexionBD _conexionBD = new ConexionBD();

        public List<ReporteReserva> ListarReservasPorFecha(DateTime fechaDesde, DateTime fechaHasta, int? idEstadoReserva)
        {
            var reservas = new List<ReporteReserva>();

            using (SqlConnection conexion = _conexionBD.CrearConexion())
            using (SqlCommand comando = new SqlCommand("SP_ReporteReservasPorFecha", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@FechaDesde", SqlDbType.Date).Value = fechaDesde.Date;
                comando.Parameters.Add("@FechaHasta", SqlDbType.Date).Value = fechaHasta.Date;
                comando.Parameters.Add("@IdEstadoReserva", SqlDbType.Int).Value =
                    idEstadoReserva.HasValue ? (object)idEstadoReserva.Value : DBNull.Value;

                conexion.Open();

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        reservas.Add(MapearReporteReserva(lector));
                    }
                }
            }

            return reservas;
        }

        private static ReporteReserva MapearReporteReserva(SqlDataReader lector)
        {
            return new ReporteReserva
            {
                IdReserva = Convert.ToInt32(lector["IdReserva"]),
                FechaReserva = Convert.ToDateTime(lector["FechaReserva"]),
                Cliente = lector["Cliente"].ToString(),
                Cancha = lector["Cancha"].ToString(),
                HoraInicio = (TimeSpan)lector["HoraInicio"],
                HoraFin = (TimeSpan)lector["HoraFin"],
                EstadoReserva = lector["EstadoReserva"].ToString(),
                PrecioBase = Convert.ToDecimal(lector["PrecioBase"]),
                PrecioFinal = Convert.ToDecimal(lector["PrecioFinal"])
            };
        }
    }
}
