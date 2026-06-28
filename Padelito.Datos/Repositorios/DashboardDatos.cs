using System;
using System.Data;
using System.Data.SqlClient;
using Padelito.Datos.Conexion;
using Padelito.Dominio.Entidades;

namespace Padelito.Datos.Repositorios
{
    public class DashboardDatos
    {
        private readonly ConexionBD _conexionBD = new ConexionBD();

        public DashboardResumen ObtenerResumen()
        {
            var resumen = new DashboardResumen();

            using (SqlConnection conexion = _conexionBD.CrearConexion())
            {
                conexion.Open();

                resumen.ClientesActivos = EjecutarEntero(conexion,
                    @"SELECT COUNT(1)
                      FROM Clientes c
                      INNER JOIN Personas p ON p.IdPersona = c.IdPersona
                      WHERE p.Activo = 1");

                resumen.CanchasActivas = EjecutarEntero(conexion,
                    @"SELECT COUNT(1)
                      FROM VW_CanchasActivas");

                resumen.ReservasDelDia = EjecutarEntero(conexion,
                    @"SELECT COUNT(1)
                      FROM Reservas
                      WHERE FechaReserva = CAST(GETDATE() AS DATE)");

                resumen.PagosRegistrados = EjecutarEntero(conexion,
                    @"SELECT COUNT(1)
                      FROM Pagos");

                resumen.IngresosRegistrados = EjecutarDecimal(conexion,
                    @"SELECT ISNULL(SUM(Monto), 0)
                      FROM Pagos");

                CargarUltimasReservas(conexion, resumen);
            }

            return resumen;
        }

        private static int EjecutarEntero(SqlConnection conexion, string consulta)
        {
            using (SqlCommand comando = new SqlCommand(consulta, conexion))
            {
                return Convert.ToInt32(comando.ExecuteScalar());
            }
        }

        private static decimal EjecutarDecimal(SqlConnection conexion, string consulta)
        {
            using (SqlCommand comando = new SqlCommand(consulta, conexion))
            {
                return Convert.ToDecimal(comando.ExecuteScalar());
            }
        }

        private static void CargarUltimasReservas(SqlConnection conexion, DashboardResumen resumen)
        {
            using (SqlCommand comando = new SqlCommand(
                @"SELECT TOP 5 IdReserva, FechaReserva, Cliente, Cancha, TipoCancha, HoraInicio,
                         HoraFin, Empleado, EstadoReserva, Promocion, PrecioBase,
                         PrecioFinal, FechaCreacion
                  FROM VW_ReservasDetalle
                  ORDER BY FechaCreacion DESC, IdReserva DESC", conexion))
            using (SqlDataReader lector = comando.ExecuteReader())
            {
                while (lector.Read())
                {
                    resumen.UltimasReservas.Add(MapearReservaDetalle(lector));
                }
            }
        }

        private static ReservaDetalle MapearReservaDetalle(SqlDataReader lector)
        {
            return new ReservaDetalle
            {
                IdReserva = Convert.ToInt32(lector["IdReserva"]),
                FechaReserva = Convert.ToDateTime(lector["FechaReserva"]),
                Cliente = lector["Cliente"].ToString(),
                Cancha = lector["Cancha"].ToString(),
                TipoCancha = lector["TipoCancha"].ToString(),
                HoraInicio = (TimeSpan)lector["HoraInicio"],
                HoraFin = (TimeSpan)lector["HoraFin"],
                Empleado = lector["Empleado"].ToString(),
                EstadoReserva = lector["EstadoReserva"].ToString(),
                Promocion = lector["Promocion"] == DBNull.Value ? "Sin promocion" : lector["Promocion"].ToString(),
                PrecioBase = Convert.ToDecimal(lector["PrecioBase"]),
                PrecioFinal = Convert.ToDecimal(lector["PrecioFinal"]),
                FechaCreacion = Convert.ToDateTime(lector["FechaCreacion"])
            };
        }
    }
}
