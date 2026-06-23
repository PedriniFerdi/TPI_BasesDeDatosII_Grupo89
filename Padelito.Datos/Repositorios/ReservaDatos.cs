using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Padelito.Datos.Conexion;
using Padelito.Dominio.Entidades;

namespace Padelito.Datos.Repositorios
{
    public class ReservaDatos
    {
        private readonly ConexionBD _conexionBD = new ConexionBD();

        public List<ReservaDetalle> ListarDetalle()
        {
            var reservas = new List<ReservaDetalle>();

            using (SqlConnection conexion = _conexionBD.CrearConexion())
            using (SqlCommand comando = new SqlCommand(
                @"SELECT IdReserva, FechaReserva, Cliente, Cancha, TipoCancha, HoraInicio,
                         HoraFin, Empleado, EstadoReserva, Promocion, PrecioBase,
                         PrecioFinal, FechaCreacion
                  FROM VW_ReservasDetalle
                  ORDER BY FechaReserva DESC, HoraInicio, Cancha", conexion))
            {
                conexion.Open();

                using (SqlDataReader lector = comando.ExecuteReader())
                {
                    while (lector.Read())
                    {
                        reservas.Add(MapearReservaDetalle(lector));
                    }
                }
            }

            return reservas;
        }

        public void Agregar(Reserva reserva)
        {
            using (SqlConnection conexion = _conexionBD.CrearConexion())
            using (SqlCommand comando = new SqlCommand(
                @"INSERT INTO Reservas
                    (IdCliente, IdTurnoDisponible, IdEmpleado, IdPromocion,
                     FechaReserva, IdEstadoReserva, PrecioBase, PrecioFinal)
                  VALUES
                    (@IdCliente, @IdTurnoDisponible, @IdEmpleado, @IdPromocion,
                     @FechaReserva, @IdEstadoReserva, @PrecioBase, @PrecioFinal)", conexion))
            {
                CargarParametros(comando, reserva);
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        public bool ExisteReservaParaTurno(DateTime fechaReserva, int idTurnoDisponible)
        {
            using (SqlConnection conexion = _conexionBD.CrearConexion())
            using (SqlCommand comando = new SqlCommand(
                @"SELECT COUNT(1)
                  FROM Reservas
                  WHERE FechaReserva = @FechaReserva
                    AND IdTurnoDisponible = @IdTurnoDisponible", conexion))
            {
                comando.Parameters.Add("@FechaReserva", SqlDbType.Date).Value = fechaReserva.Date;
                comando.Parameters.Add("@IdTurnoDisponible", SqlDbType.Int).Value = idTurnoDisponible;
                conexion.Open();

                return Convert.ToInt32(comando.ExecuteScalar()) > 0;
            }
        }

        public void CambiarEstado(int idReserva, int idEstadoReserva)
        {
            using (SqlConnection conexion = _conexionBD.CrearConexion())
            using (SqlCommand comando = new SqlCommand("SP_CambiarEstadoReserva", conexion))
            {
                comando.CommandType = CommandType.StoredProcedure;
                comando.Parameters.Add("@IdReserva", SqlDbType.Int).Value = idReserva;
                comando.Parameters.Add("@IdEstadoReserva", SqlDbType.Int).Value = idEstadoReserva;
                conexion.Open();
                comando.ExecuteNonQuery();
            }
        }

        private static void CargarParametros(SqlCommand comando, Reserva reserva)
        {
            comando.Parameters.Add("@IdCliente", SqlDbType.Int).Value = reserva.IdCliente;
            comando.Parameters.Add("@IdTurnoDisponible", SqlDbType.Int).Value = reserva.IdTurnoDisponible;
            comando.Parameters.Add("@IdEmpleado", SqlDbType.Int).Value = reserva.IdEmpleado;
            comando.Parameters.Add("@IdPromocion", SqlDbType.Int).Value =
                reserva.IdPromocion.HasValue ? (object)reserva.IdPromocion.Value : DBNull.Value;
            comando.Parameters.Add("@FechaReserva", SqlDbType.Date).Value = reserva.FechaReserva.Date;
            comando.Parameters.Add("@IdEstadoReserva", SqlDbType.Int).Value = reserva.IdEstadoReserva;
            comando.Parameters.Add("@PrecioBase", SqlDbType.Decimal).Value = reserva.PrecioBase;
            comando.Parameters["@PrecioBase"].Precision = 10;
            comando.Parameters["@PrecioBase"].Scale = 2;
            comando.Parameters.Add("@PrecioFinal", SqlDbType.Decimal).Value = reserva.PrecioFinal;
            comando.Parameters["@PrecioFinal"].Precision = 10;
            comando.Parameters["@PrecioFinal"].Scale = 2;
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
